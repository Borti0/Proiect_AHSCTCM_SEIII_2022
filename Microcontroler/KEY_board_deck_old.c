#include "defines.h"
#include "functions.h"

int main(void)
{
    stdio_init_all();

    sleep_ms(30000);
    printf("...Start Configuration...\n");
    set_uart(UART_TX_PIN, UART_RX_PIN, BAUD_RATE);

    printf("...Setting GPIO for device:\n");
    set_out_pins(GLOBAL_OUT_MASK);
    set_in_pins(GLOBAL_IN_MASK);

    set_pull_down_pins(button_row);
    set_pull_down_pins(led_row);

    /*printf("\t>>Refresh all input pins\n");
    gpio_init_mask(GLOBAL_IN_MASK);

    printf("\t>>Set direction of input pins\n");
    gpio_set_dir_in_masked(GLOBAL_IN_MASK);

    printf("\t>>Select pull down resistor of input pins\n");
    gpio_pull_down(BUTTON_MATRIX_R_1);
    gpio_pull_down(BUTTON_MATRIX_R_1+1);

    printf(">>>Set all out pins:\n");
    printf("\t>>Refresh all output pins\n");
    gpio_init_mask(GLOBAL_OUT_MASK);

    printf("\t>>Set direction for input pins\n");
    gpio_set_dir_out_masked(GLOBAL_OUT_MASK);

    printf("\t>>Set pull down resistor of led matrix\n");
    gpio_pull_down(LED_MATRIX_R_1);
    gpio_pull_down(LED_MATRIX_R_1+1);*/

    printf("... ALL USED GPIO IS SETTED\n");
    
    /*printf("...Test LED matrix\n");
    int pages = 1, sense = 0, flag = 0;
    while (!flag)
    {
        printf("\t>>PAGE %d\n", pages);
        gpio_clr_mask(ALL_PINS_LED_MATRIX);
        switch (pages)
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
        case 4:
            gpio_set_mask(led_row_mask[1]);
            gpio_set_mask(led_columns_mask[0]);
            break;
        }
        
        if(!sense)
            pages++;
        else
            pages--;
        if(pages == 4)
            sense = 1;
        if(pages == 0)
            flag = 1;
        sleep_ms(500);
    }

    printf("...LEDS ARE TESTED\n");
    gpio_clr_mask(ALL_PINS_LED_MATRIX);

    int i = 0, ret = 0;
    pages = 0;
    while (1)
    {
        printf("...Whait for button\n");
        gpio_clr_mask(ALL_COLUMNS_BUTTON_MATRIX);
        gpio_set_mask(button_columns_mask[pages]);
        if( (ret =(gpio_get_all() & 0xC)) != 0 )
        {
            printf("\t>>BUTTON %d\n", pages);
            printf("value of gpio %d\n", gpio_get_all() & ALL_COLUMNS_BUTTON_MATRIX);
        }
        else
        {
            printf("\t>>result %d\n", ret);
        }
        if(pages == 3)
            pages = 0;
        else
            pages ++;
        sleep_ms(1000);
        i++;
        printf("test spend secons:\n\t>>%d[s]\n", i);
    }*/
}

