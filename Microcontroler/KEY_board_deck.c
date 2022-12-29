#include "defines.h"
#include "functions.h"

const char page_mask[] = {
    0b01000000, //page 1
    0b10000000, //page 2
    0b11000000, //page 3
};

const uint button_comand_mask[] = {
    0b00000001, //button 1
    0b00000010, //button 2
    0b00000100, //button 3
    0b00001000, //button 4
    0b00010000, //button 5
    0b00100000  //button 6
};

const uint button_columns_mask[] = {
    0x40, 0x80, 0x100, 0x200
};

const uint led_row_mask[] = {
    0x400, 0x800
};

const uint led_columns_mask[] = {
    0x4000, 0x8000
};

const uint button_row[] = {
    BUTTON_MATRIX_R_1,
    BUTTON_MATRIX_R_1+1
};

const uint led_row[] = {
    LED_MATRIX_R_1,
    LED_MATRIX_R_1+1
};

static volatile bool can_read_buttons = true;
static volatile bool new_message = false;
static volatile bool reset_me = false;


static char page_index = 1;
static char message_command = 0;
static char connect_message = 0;


static void timer_callback_irq(void);
static void set_timer_in_us(uint32_t);
static void gpio_callback(uint);

static void set_uart_rx_irq(void);
static void uart_rx_irq(void);


int main (void)
{
    stdio_init_all();

    sleep_ms(30000);
    printf("...Start Configuration...\n");
    set_uart(UART_TX_PIN, UART_RX_PIN, BAUD_RATE);
    uart_puts(UART_ID, "CONECT TO UART\n");

    printf("try to connect via uart\n");
    uart_connect();

    set_uart_rx_irq();
    printf("...Setting GPIO for device:\n");
    set_out_pins(GLOBAL_OUT_MASK);
    set_in_pins(GLOBAL_IN_MASK);

    set_pull_down_pins(button_row);
    set_pull_down_pins(led_row);

    test_led_matrix(led_row_mask, led_columns_mask);

    printf("...Enable button matrix row IRQ\n");
    gpio_set_irq_enabled_with_callback(2, GPIO_IRQ_EDGE_RISE, true, &gpio_callback);
    gpio_set_irq_enabled_with_callback(3, GPIO_IRQ_EDGE_RISE, true, &gpio_callback);

    int pages = 0, ret = 0;
    char msg = 0;
    while(1)
    {
        if (reset_me == false)
        {
            printf("<<<cicle %d >>>\n", ret++);
            //button shifting
            gpio_clr_mask(ALL_COLUMNS_BUTTON_MATRIX);
            gpio_set_mask(button_columns_mask[pages]);
            if(pages == 3)
                pages = 0;
            else
                pages ++;

            //page showing
            gpio_clr_mask(ALL_PINS_LED_MATRIX);
            switch(page_index)
            {
            case 1:
                gpio_set_mask(led_row_mask[0]);
                gpio_set_mask(led_columns_mask[1]);
                break;
            case 2:
                gpio_set_mask(led_row_mask[0]);
                gpio_set_mask(led_columns_mask[0]);
                break;
            case 3:
                gpio_set_mask(led_row_mask[1]);
                gpio_set_mask(led_columns_mask[1]);
                break;
            }
            //show worning 
            if(can_read_buttons == false)
            {
                gpio_clr_mask(ALL_PINS_LED_MATRIX);
                gpio_set_mask(led_row_mask[1]);
                gpio_set_mask(led_columns_mask[0]);
            }
        }
        else
        {
            gpio_clr_mask(ALL_PINS_LED_MATRIX);
            gpio_clr_mask(ALL_COLUMNS_BUTTON_MATRIX);
            irq_set_enabled(UART_IRQ, false);
            gpio_set_irq_enabled(2, GPIO_IRQ_EDGE_RISE, false);
            gpio_set_irq_enabled(3, GPIO_IRQ_EDGE_RISE, false);
            uart_connect();
            gpio_set_irq_enabled(2, GPIO_IRQ_EDGE_RISE, true);
            gpio_set_irq_enabled(3, GPIO_IRQ_EDGE_RISE, true);
            irq_set_enabled(UART_IRQ, true);
            reset_me = false;
        }
        
        if(new_message == true)
        {
            printf("...Message is %d\n", message_command);
            uart_putc(UART_ID, message_command);
            new_message = false;
        }
    }
}

static void timer_callback_irq(void)
{
    hw_clear_bits(&timer_hw->intr, 1u << ALARM_NUM);
    printf(">>>NOW CAN READ NEW BUTTON\n");
    can_read_buttons = true;
}

static void set_timer_in_us(uint32_t delay_us)
{
    hw_set_bits(&timer_hw->inte, 1u << ALARM_NUM);
    irq_set_exclusive_handler(ALARM_IRQ, timer_callback_irq);
    irq_set_enabled(ALARM_IRQ, true);
    uint64_t target = timer_hw->timerawl + delay_us;
    timer_hw->alarm[ALARM_NUM] = (uint32_t) target;
}

static void gpio_callback(uint gpio)
{
    if(can_read_buttons == true)
    {
        can_read_buttons = false;

        uint32_t columns = (gpio_get_all() & ALL_COLUMNS_BUTTON_MATRIX);

        message_command &= 0;
        message_command |= page_mask[page_index-1];
        if(gpio == 3)
        {
            switch (columns)
            {
            case 512:
                if(page_index < 3)
                    page_index++;
                else
                    page_index = 3;
                printf(">>>Page is %d\n", page_index);
                break;
            case 64:
                printf(">>>Button 4\n");
                message_command |= button_comand_mask[BUTTON4];
                new_message = true;
                break;
            case 128:
                printf(">>>Button 5\n");
                message_command |= button_comand_mask[BUTTON5];
                new_message = true;
                break;
            case 256:
                printf(">>>Button 6\n");
                message_command |= button_comand_mask[BUTTON6];
                new_message = true;
                break;
            default:
                break;
            }
        }

        if(gpio == 2)
        {
            switch (columns)
            {
            case 512:
                if(page_index > 1)
                    page_index --;
                else
                    page_index = 1;
                printf(">>>Page is %d\n", page_index);
                break;
            case 64:
                printf(">>>Button 1\n");
                message_command |= button_comand_mask[BUTTON1];
                new_message = true;
                break;
            case 128:
                printf(">>>Button 2\n");
                message_command |= button_comand_mask[BUTTON2];
                new_message = true;
                break;
            case 256:
                printf(">>>Button 3\n");
                message_command |= button_comand_mask[BUTTON3];
                new_message = true;
                break;
            default:
                break;
            }
        }

        printf("\n");
        set_timer_in_us(500000);
    }
}

static void set_uart_rx_irq(void)
{
    irq_set_exclusive_handler(UART_IRQ, uart_rx_irq);
    irq_set_enabled(UART_IRQ, true);
    uart_set_irq_enables(UART_ID, true, false);
}

static void uart_rx_irq(void)
{
    char msg = 0;
    if(uart_is_readable_within_us(UART_ID, 500))
    {
        msg = uart_getc(UART_ID);
        if(msg == '0')
        {
            reset_me = true;
        }
        msg = 0;
    }
}
