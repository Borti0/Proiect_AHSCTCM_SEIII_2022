#include "hardware/uart.h"
#include "hardware/timer.h"
#include "hardware/irq.h"

#include "pico/stdlib.h"
#include "hardware/uart.h"
#include "hardware/gpio.h"

#define UART_IRQ UART0_IRQ
#define UART_ID uart0
#define BAUD_RATE 9600
#define DATA_BITS 8
#define STOP_BITS 2
#define PARITY UART_PARITY_NONE

#define UART_TX_PIN 0
#define UART_RX_PIN 1

#define ALARM_NUM 0
#define ALARM_IRQ TIMER_IRQ_0

#define BUTTON_MATRIX_R_1 2
#define LED_MATRIX_R_1 10

#define GLOBAL_IN_MASK 0xC
#define GLOBAL_OUT_MASK 0xCFC0

#define ALL_COLUMNS_BUTTON_MATRIX 0x03C0
#define ALL_PINS_LED_MATRIX 0xCC00

#define BUTTON1 0
#define BUTTON2 1
#define BUTTON3 2
#define BUTTON4 3
#define BUTTON5 4
#define BUTTON6 5