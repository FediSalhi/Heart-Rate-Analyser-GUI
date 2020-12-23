using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heart_Rate_Analyser
{
    public enum Packet_capturing_status_e
    {
        CAPTURING_SYNCHRONOUS_1,
        CAPTURING_SYNCHRONOUS_2,
        CAPTURING_SOURCE_DEVICE,
        CAPTURING_TARGET_DEVICE,
        CAPTURING_PACKET_TYPE,
        CAPTURING_DATA_LENGTH,
        CAPTURING_DATA
    }

    public enum Packet_types_e
    {
        HEART_RATE_MEASUREMENT_PACKET
    }

    public struct Simple_communication_packet_t
    {
        public byte synchronous_1_u8;
        public byte synchronous_2_u8;
        public byte source_device_u8;
        public byte target_device_u8;
        public byte paket_type_u8;
        public byte data_length_u8;
        public byte[] data_u8;
    }

    public struct Serial_uart_channel_t
    {
        public Packet_capturing_status_e paket_capturing_status_t;
        public short captured_packet_number_u16;
        public short solved_packet_number_u16;
        public Simple_communication_packet_t simple_com_packet_t;
        //void (*packet_solver)(Simple_communication_packet_t*); implement packet_solver
    }


    public class Simple_Communication
    {

        public const byte SYNCHRONOUS1      = 0x11;
        public const byte SYNCHRONOUS2      = 0x99;
        public const byte MAX_DATA_LENGTH   = 255;
        public const byte PC                = 0x00;
        public const byte MSP430            = 0x01;

        public const byte HEART_RATE_MEASUREMENT_PACKET       = 0x00;


        public static void packet_solver()
        {
            // to be implemented, from packet.data to measurement global, 
            // birlestir fonk
        }

        public static short control_if_there_is_data_to_capture()
        {
            short data_received_s16 = 0;
            if (Global_Variables.GL_buffer_last_u32 == Global_Variables.GL_buffer_first_u32)
            {
                data_received_s16 = -1;
            }
            else if (Global_Variables.GL_buffer_last_u32 - Global_Variables.GL_buffer_first_u32 <= 0)
            {
                data_received_s16 = (short)(Global_Variables.GL_buffer_last_u32 - Global_Variables.GL_buffer_first_u32 + Simple_Communication.MAX_DATA_LENGTH);
            }
            else
            {
                data_received_s16 = (short)(Global_Variables.GL_buffer_last_u32 - Global_Variables.GL_buffer_first_u32);
            }
            return data_received_s16;
        }

        public static void capture_packet_from_bytes()
        {
            ushort byte_index_u16 = 0;
            ushort data_index_u16 = 0;
            short number_of_recieved_bytes = control_if_there_is_data_to_capture();

            if (number_of_recieved_bytes > 0)
            {
                /* buffer is not empty */

                for (byte_index_u16 = 0; byte_index_u16 < number_of_recieved_bytes; byte_index_u16++)
                {
                    switch (Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t)
                    {
                        case Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1:
                        {
                            if (Global_Variables.GL_rx_buffer_u8[byte_index_u16] == SYNCHRONOUS1)
                            {
                                /* recived byte is synchronous 1 */
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.synchronous_1_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_2;
                                Global_Variables.GL_buffer_first_u32++;

                                if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                                {
                                    Global_Variables.GL_buffer_first_u32 = 0;
                                }
                            }
                            break;
                        }

                        case Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_2:
                        {
                            if (Global_Variables.GL_rx_buffer_u8[byte_index_u16] == SYNCHRONOUS2)
                            {
                                /* recived byte is synchronous 1 */
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.synchronous_2_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SOURCE_DEVICE;
                                Global_Variables.GL_buffer_first_u32++;

                                if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                                {
                                    Global_Variables.GL_buffer_first_u32 = 0;
                                }
                            }
                            break;
                        }

                        case Packet_capturing_status_e.CAPTURING_SOURCE_DEVICE:
                        {
                            if (Global_Variables.GL_rx_buffer_u8[byte_index_u16] == MSP430)
                            {
                                /* recived byte is synchronous 1 */
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.source_device_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_TARGET_DEVICE;
                                Global_Variables.GL_buffer_first_u32++;

                                if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                                {
                                        Global_Variables.GL_buffer_first_u32 = 0;
                                }
                             }
                             break;
                        }

                        case Packet_capturing_status_e.CAPTURING_TARGET_DEVICE:
                        {
                            if (Global_Variables.GL_rx_buffer_u8[byte_index_u16] == MSP430)
                            {
                                /* recived byte is synchronous 1 */
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.source_device_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_PACKET_TYPE;
                                Global_Variables.GL_buffer_first_u32++;

                                if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                                {
                                    Global_Variables.GL_buffer_first_u32 = 0;
                                }
                            }
                            break;
                        }

                        case Packet_capturing_status_e.CAPTURING_PACKET_TYPE:
                        {
                            if (Global_Variables.GL_rx_buffer_u8[byte_index_u16] == HEART_RATE_MEASUREMENT_PACKET)
                            {
                                /* recived byte is synchronous 1 */
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.paket_type_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_DATA_LENGTH;
                                Global_Variables.GL_buffer_first_u32++;

                                if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                                {
                                    Global_Variables.GL_buffer_first_u32 = 0;
                                }
                            }
                            break;
                        }

                        case Packet_capturing_status_e.CAPTURING_DATA_LENGTH:
                        {
                            /* recived byte is synchronous 1 */
                            Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_length_u8 = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_DATA;
                            Global_Variables.GL_buffer_first_u32++;

                            if (Global_Variables.GL_buffer_first_u32 > Simple_Communication.MAX_DATA_LENGTH)
                            {
                                Global_Variables.GL_buffer_first_u32 = 0;
                            }
                            
                            break;
                        }

                        case Packet_capturing_status_e.CAPTURING_DATA:
                        {
                                /* recived byte is synchronous 1 */
                            for (data_index_u16 = 0; data_index_u16 < Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_length_u8; data_index_u16++)
                            {
                                Global_Variables.GL_serial_uart_channel_t.simple_com_packet_t.data_u8[data_index_u16] = Global_Variables.GL_rx_buffer_u8[Global_Variables.GL_buffer_first_u32];
                                Global_Variables.GL_buffer_first_u32++;
                            }
                              
                            Global_Variables.GL_serial_uart_channel_t.paket_capturing_status_t = Packet_capturing_status_e.CAPTURING_SYNCHRONOUS_1;
                            Global_Variables.GL_buffer_first_u32++;

                            if (Global_Variables.GL_buffer_first_u32 >= Simple_Communication.MAX_DATA_LENGTH)
                            {
                                Global_Variables.GL_buffer_first_u32 = 0;
                            }

                            // add solve packet here

                            break;
                        }

                    }
                }
                
                


            }
        }




    }
}
