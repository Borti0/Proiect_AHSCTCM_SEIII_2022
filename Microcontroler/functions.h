#include <stdio.h>

#include "defines.h"

#define _COUNTOF(x) (sizeof(x)/sizeof(x[0]))

void set_uart (uint, uint, uint);

void set_out_pins(uint);
void set_in_pins(uint);

void set_pull_down_pins(uint *);
void test_led_matrix(uint *, uint *);

void uart_connect(void);