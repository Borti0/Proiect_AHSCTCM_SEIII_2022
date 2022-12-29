#include "functions.h"

void set_uart(uint tx_pin, uint rx_pin, uint baud_rate)
{
    printf(">Configurate Uart module\n");
    if(uart_init(UART_ID, baud_rate))
        printf(">>Uart module set corect\n");
    else
        printf(">>Uart module set incorect\n");
    
    printf(">>Init uart pins\n");
    gpio_init(tx_pin);
    gpio_init(rx_pin);

    printf(">>Set uart function for pins\n");
    gpio_set_function(tx_pin, GPIO_FUNC_UART);
    gpio_set_function(rx_pin, GPIO_FUNC_UART);

    printf(">>Turn off flow control\n");
    uart_set_hw_flow(UART_ID, false, false);

    printf(">>Set UART data format\n");
    uart_set_format(UART_ID, DATA_BITS, STOP_BITS, PARITY);

    printf(">>Disable UART FIFO\n");
    uart_set_fifo_enabled(UART_ID, false);

    printf(">Uart %d is Configurate\n\n", UART_ID);
}

void set_out_pins(uint out_mask)
{
    printf(">Set OUT PINS\n");
    printf(">>INIT OUT PINS\n");
    gpio_init_mask(out_mask);
    printf(">>Set Direction of Out Pins\n\n");
    gpio_set_dir_out_masked(out_mask);
}

void set_in_pins(uint in_mask)
{
    printf(">Set IN PINS\n");
    printf(">>INIT IN PINS\n");
    gpio_init_mask(in_mask);
    printf(">>Set Direction of In Pins\n\n");
}

void set_pull_down_pins(uint * down_pins)
{
    int nr_pins = _COUNTOF(down_pins);
    printf(">Set pull down resistors for %d pins\n", nr_pins+1);
    for (int pin = 0 ; pin < nr_pins+1 ; pin++)
    {
        printf(">>Pin %d is pulled down\n", down_pins[pin]);
        gpio_pull_down(down_pins[pin]);
    }
    printf("\n");
}

void test_led_matrix(uint * row, uint * columns)
{
    const uint _nr_rows = _COUNTOF(row) + 1;
    const uint _nr_columns = _COUNTOF(columns) + 1;

    printf(">TEST %dx%d led matrix\n", _nr_rows, _nr_columns);
    
    int page = 1, sense = 0, flag = 0;
    while(!flag)
    {
        printf(">>LED %d\n", page);
        gpio_clr_mask(ALL_PINS_LED_MATRIX);
        switch (page)
        {
        case 1:
            gpio_set_mask(row[0]);
            gpio_set_mask(columns[1]);
            break;
        case 2:
            gpio_set_mask(row[0]);
            gpio_set_mask(columns[0]);
            break;
        case 3:
            gpio_set_mask(row[1]);
            gpio_set_mask(columns[1]);
            
            break;
        case 4:
            gpio_set_mask(row[1]);
            gpio_set_mask(columns[0]);
            break;
        }
        
        if(!sense)
            page++;
        else
            page--;
        if(page == 4)
            sense = 1;
        if(page == 0)
            flag = 1;
        sleep_ms(500);
    }

    gpio_clr_mask(ALL_PINS_LED_MATRIX);
}

void uart_connect(void)
{
    volatile bool PC_FINDE = false;
    char connect = 0;
    while(!PC_FINDE)
    {
        //uart_puts(UART_ID, "TRY TO CONNECT\n");
        if(uart_is_readable_within_us(UART_ID, 500))
        {
            connect = uart_getc(UART_ID);
            if(connect == '1')
            {
                PC_FINDE = true;
                char send = '1';
                uart_putc(UART_ID, send);
            }else
            {
                printf(">>>!Incorect connect message\n");
            }
            connect = 0;
        }
    }
    PC_FINDE = false;
}