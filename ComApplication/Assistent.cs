using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace ComApplication
{
    // "общий" класс, для арифметической и логической обработки данных
    static class Assist // Assistent
    {

        // возвращает строку, длина которой не меньше length; недостаток заполняет пробелами
        public static string StringByLength(string data, int length, bool isCutIfLengthSmaller = false)
        {
            if (isCutIfLengthSmaller & (data.Length > data.Length)) return(data.Remove(length));
            for (int ii = data.Length; ii < length; ii++)
                data += " ";
            return (data);
        }
                
        // добавляет byte к bytes[]
        public static byte[] AddByteToBytes(byte b, byte[] bytes)
        {
            if (bytes == null) 
                return(new byte[b]);
            byte[] res = new byte[bytes.Count() + 1];
            Array.Copy(bytes, 0, res, 0, bytes.Length);
            res[bytes.Count()] = b;
            return (res);
        }

        // разбивает byte[] на несколько массивов согласно макс. кол-во байт в каждой строке данных
        // 1 вариант - с данными, которые сохранены
        // 2 вариант - с индивидуальными данными
        public static byte[][] BytesSplitByKolBytes(byte[] val) { return (BytesSplitByKolBytes(val, OUnit.getKolBytesPerLine)); }
        public static byte[][] BytesSplitByKolBytes(byte[] val, int kolBytesPerLine)
        {
            byte[][] res = new byte[(byte)Math.Ceiling((double)val.Length / kolBytesPerLine)][];
            int indexResFirst = 0;
            for (int ii = 0; ii < res.Length - 1; ii++)
            {
                res[ii] = new byte[kolBytesPerLine];
                Array.Copy(val, indexResFirst, res[ii], 0, kolBytesPerLine);
                indexResFirst += kolBytesPerLine;
            }
            res[res.Length-1] = new byte[val.Length - indexResFirst];
            Array.Copy(val, indexResFirst, res[res.Length-1], 0, val.Length - indexResFirst);
            return (res);
        }

        // разбивает byte[] на несколько массивов согласно разделителю byte[] simbolsRazdPaketByPosledovSimbols
        public static byte[][] BytesSplitByRazdPaket(byte[] val, byte[] razdBytes)
        {
            int cnt = 0; // считаем количество разделителей, чтоб создать byte[][] res;
            int i_temp1 = 0;
            while (i_temp1 < val.Count())
            {
                if (IsHereIsRazd(i_temp1, val, razdBytes))
                {
                    i_temp1 += razdBytes.Count();
                    cnt++;
                }
                else
                    i_temp1++;
            }
            cnt += 1;
            // разносим в byte[][]
            byte[][] res = new byte[cnt][];
            // индекс первого элемента res[ii] в val
			int firstIndex = 0; 
            // перебор byte[ii][]
			for (int ii = 0; ii < cnt-1; ii++) 
            {
                for (int ll = firstIndex; ll < val.Count(); ll++)
                    // нашли индекс разделителя в val
				    if (IsHereIsRazd(ll, val, razdBytes)) 
                    {
                        // копируем данные 
						res[ii] = new byte[ll-firstIndex]; 
                        Array.Copy(val, firstIndex, res[ii], 0, ll - firstIndex);
                        // запоминаем индекс
						firstIndex = ll + razdBytes.Length; 
                        break;
                    }
            }
            // копируем данные после последнего разделителя
            res[res.Count() - 1] = new byte[val.Count() - firstIndex]; 
            Array.Copy(val, firstIndex, res[res.Length - 1], 0, val.Length - firstIndex);
            return (res);
        }

        // находится ли в заданном индексе массива index элементы val, полностью идентичные элементам массива razdBytes
        static private bool IsHereIsRazd(int index, byte[] val, byte[] razdBytes)
        {
            try
            {
                if ((val.Length == 0) | (razdBytes.Length == 0) | (index >= val.Length))
                    return (false);
                bool isHereIsRazd = true;
                for (int ii = 0; ii < (((val.Count() - index) < razdBytes.Count()) ? (val.Count() - index) : razdBytes.Count()); ii++)
                    if (val[index + ii] != razdBytes[ii])
                    {
                        isHereIsRazd = false; ;
                        break;
                    }
                return (isHereIsRazd);
            }
            catch
            {
                return (false);
            }
        }

        // проверяет тект на корректность введённых hex-данных
        public static string GetVerifyHexText(byte[] val, char razd, bool isRazdByte, bool isRazd8Byte)
        {
            return(GetVerifyHexText(BitConverter.ToString(val), razd, isRazdByte, isRazd8Byte));
        }
        //                                             разделитель, разделять ли байты, разделять ли каждые 8 байт
        public static string GetVerifyHexText(string val, char razd, bool isRazdByte, bool isRazd8Byte)
        {
            string s_AsIs = GetFilteredHex(val);
            // расставляем разделители
            string res = "";
            for (int i = 1; i < s_AsIs.Count() + 1; i += 2)
            {
                res += s_AsIs[i - 1];
                if (i <= (s_AsIs.Count() - 1))
                    res += s_AsIs[i];
                if (isRazd8Byte)
                {
                    if (i < (s_AsIs.Count() - 1))
                    {
                        // 1 байт = 2 знака => 8 байт = 16 знаков
						if (((i + 1) % 16) == 0) 
                            res += " " + Properties.Settings.Default.razdHex8 + " ";
                        else
                            if (isRazdByte)
                                res += razd;
                    }
                }
                else
                    if (isRazdByte)
                        if (i < (s_AsIs.Count() - 1))
                            res += razd;
            }
            return(res);
        }

        // проверяет textbox на корректность введённых hex-данных
        //                                            разделитель, разделять ли байты, разделять ли каждые 8 байт
        public static bool VerifyTextbox(TextBox tb, char razd, bool isRazdByte, bool isRazd8Byte)
        {
            try
            {
                // запоминаем где был курсор                
				int cursPosFromEnd = tb.Text.Count() - tb.SelectionStart; 
                string res = GetVerifyHexText(tb.Text, razd, isRazdByte, isRazd8Byte);
                // вставляем в textbox
                tb.Text = res;
                if (tb.Text.Count() - cursPosFromEnd >= 0)
                    tb.SelectionStart = tb.Text.Count() - cursPosFromEnd;
                return (true);
            }
            catch 
            {
                return (false);
            }
        }

        // из hex-строки возвращает только hex-символы
        public static string GetFilteredHex(string val)
        {
            // текст, без маски и профильтрованный
			string s_AsIs = ""; 
            // маска для фильтра
			string mask = "0123456789ABCDEF"; 
            foreach (char c in val.Trim().ToUpper())
                if (mask.IndexOf(c) != -1)
                    s_AsIs += c;
            return (s_AsIs);
        }

        // возвращает время согласно заданного формата
        public static string GetTimeNowFormat() { return (GetTimeFormat(DateTime.Now, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isTimeFormatPodpis, Properties.Settings.Default.isShowTimeMS)); }
        public static string GetTimeFormat(DateTime data) { return (GetTimeFormat(data, Properties.Settings.Default.timeFormatIndex, Properties.Settings.Default.isTimeFormatPodpis, Properties.Settings.Default.isShowTimeMS)); }
        public static string GetTimeFormat(DateTime data, int timeFormatIndex, bool isMS, bool isTimeFormatPodpis)
        {
            // timeFormat.Add(0, "дд:ММ:гг чч:мм:сс");
            // timeFormat.Add(1, "дд:ММ чч:мм:сс");
            // timeFormat.Add(2, "дд чч:мм:сс");
            // timeFormat.Add(3, "чч:мм:сс");
            // timeFormat.Add(4, "мм:сс");
            // timeFormat.Add(5, "сс");
            try
            {
                switch (timeFormatIndex)
                {
                    case 0:
                        if (isTimeFormatPodpis)
                            return (data.Day.ToString("00") + "." + 
                                    data.Month.ToString("00") + "." +
                                    data.Year.ToString() + "г" + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" + 
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Day.ToString("00") + "." +
                                    data.Month.ToString("00") + "." +
                                    data.Year.ToString() + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    case 1:
                        if (isTimeFormatPodpis)
                            return (data.Day.ToString("00") + "." +
                                    data.Month.ToString("00") + "М" + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Day.ToString("00") + "." +
                                    data.Month.ToString("00") + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    case 2:
                        if (isTimeFormatPodpis)
                            return (data.Day.ToString("00") + "д" + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Day.ToString("00") + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    case 3:
                        if (isTimeFormatPodpis)
                            return (data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    case 4:
                        if (isTimeFormatPodpis)
                            return (data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    case 5:
                        if (isTimeFormatPodpis)
                            return (data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : "") + "с");
                        else
                            return (data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                    default:
                        return (data.Day.ToString() + "." +
                                    data.Month.ToString() + "." +
                                    data.Year.ToString() + " " +
                                    data.Hour.ToString("00") + ":" +
                                    data.Minute.ToString("00") + ":" +
                                    data.Second.ToString("00") +
                                    (isMS ? "." + data.Millisecond.ToString("000") : ""));
                }
            }
            catch
            {
                return ("Ошибка_в_GetTimeNowFormat");
            }
        }
        
        // преобразовывает строку: из text в hex
        public static string StringFromTextToFilteredHex(string val)
        {
            try
            {
                Encoding enc = Encoding.GetEncoding(OUnit.getCodePage.kodStranicaNomer);
                return (BitConverter.ToString(enc.GetBytes(val)).Replace("-", ""));
            }
            catch
            {
                return ("Ошибка_в_TextToHex");
            }
        }

        // преобразовывает строку hex в byte[]
        public static byte[] BytesFromFilteredHex(string val)
        {
            try
            {
                string[] ss_temp2;
                int i_temp1 = 1;
                ss_temp2 = new string[(int)Math.Ceiling((double)val.Count() / 2)];
                while (i_temp1 < val.Count())
                {
                    ss_temp2[(int)((i_temp1 - 1) / 2)] = val[i_temp1 - 1].ToString() + val[i_temp1].ToString();
                    if (i_temp1 == val.Count() - 2) // на случай нечётного числа символов
                        ss_temp2[ss_temp2.Count() - 1] = "0" + val[val.Count() - 1];
                    i_temp1 += 2;
                }
                byte[] b_temp2 = new byte[ss_temp2.Count()];
                for (int ii = 0; ii < ss_temp2.Count(); ii++)
                    byte.TryParse(ss_temp2[ii], System.Globalization.NumberStyles.HexNumber, null, out b_temp2[ii]);
                return (b_temp2);
            }
            catch
            {
                return(new byte[] {});
            }

        }

        // преобразовывает строку: из hex в text
        public static string StringFromHexTextBoxCommandToText(string val)
        {
            return (StringFromHexTextBoxCommandToText(val, OUnit.getCodePage));
        }
        public static string StringFromHexTextBoxCommandToText(byte[] val, structCodePage kodPage)
        {
            return (StringFromHexTextBoxCommandToText(BitConverter.ToString(val), kodPage));
        }
        public static string StringFromHexTextBoxCommandToText(string val, structCodePage kodPage)
        {
            try
            {
                string s_AsIs = GetFilteredHex(val);
                Encoding enc = Encoding.GetEncoding(kodPage.kodStranicaNomer); // https:// msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx
                return (enc.GetString(BytesFromFilteredHex(s_AsIs)));
            }
            catch
            {
                return ("Ошибка_в_HexToText");
            }
        }

        // разбивает строку с переменной на: до, переменная
        public static string[] Extract1data(string data_string, string razd) 
        {
            // считаем количество разделителей, чтоб создать string[] res;
			int cnt = 0; 
            string s_temp = data_string;
            while (s_temp.Contains(razd))
            {
                cnt += 1;
                s_temp = s_temp.Remove(0, s_temp.IndexOf(razd) + razd.Length);
            }
            cnt += 1;
            string[] res;
            if (cnt >= 1)
                res = new string[cnt];
            else
                res = new string[1];
            int i_temp1;
            s_temp = data_string;
            for (i_temp1 = 0; i_temp1 < cnt; i_temp1++)
            {
                if (s_temp.IndexOf(razd) != -1)
                {
                    res[i_temp1] = s_temp.Substring(0, s_temp.IndexOf(razd));
                    s_temp = s_temp.Remove(0, s_temp.IndexOf(razd) + razd.Length);
                }
                else
                    res[i_temp1] = s_temp;
            }
            return (res);
        }

        // возвращает byte[], где каждые 2 последовательных байта (16-битный регистр) поменены местами
        // little-endian to big-endian // в каждом 16-битном регистре прямой порядок переводим в обратный порядок
        public static Boolean GetReverse16bit(ref Byte[] b_res, Byte[] b_dan, int index, int count)
        {
            if (b_dan.Count() < (index + 1) + count)
                return (false);
            b_res = new Byte[count];
            int i_temp1 = 1;
            while (i_temp1 < b_res.Count())
            {
                b_res[i_temp1 - 1] = b_dan[index + i_temp1];
                b_res[i_temp1] = b_dan[index + i_temp1 - 1];
                i_temp1 += 2;
            }
            return (true);
        }

        // возвращает массив, дополненный кодом CRC
        public static Byte[] GetBytesWithCRC16modbus(Byte[] b_mas)
        {
            Byte[] b_res = new Byte[b_mas.Count() + 2];
            int i_temp1;
            for (i_temp1 = 0; i_temp1 < b_mas.Count(); i_temp1++)
                b_res[i_temp1] = b_mas[i_temp1];
            Byte[] crc16 = CalcCRC16(b_mas);
            b_res[b_mas.Count()] = crc16[0];
            b_res[b_mas.Count() + 1] = crc16[1];
            return (b_res);
        }

        // считает CRC для массива
        public static Byte[] CalcCRC16(Byte[] b_mas)
        {
            int i_temp1;
            UInt16 res = 0xFFFF;
            foreach (Byte b in b_mas)
            {
                res ^= b;
                for (i_temp1 = 0; i_temp1 < 8; i_temp1++)
                {
                    if (res % 2 == 1)
                    {
                        res >>= 1;
                        res ^= 0xA001;
                    }
                    else res >>= 1;
                }
            }
            return (BitConverter.GetBytes(res));
        }

    }
}
