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
    #region структуры и простые классы хранения

    // пакет отправленных/принятых данных
    public struct structPacketBytes
    {
        // данные
		public byte[] dataBytes;            
        // время отправки/приёма данных
		public DateTime timeDone;           
        // была ли ошибка при отправке/приёме данных
		public bool isResultError;          
        // диагностическое сообщение отправки данных
		public string resultDiagnosticMessage;

        //  Constructor:
        public structPacketBytes(byte[] par_dataBytes, bool par_isResultError, string par_resultDiagnosticMessage) 
        {
            this.dataBytes = par_dataBytes;
            this.timeDone = DateTime.Now;
            this.isResultError = par_isResultError;
            this.resultDiagnosticMessage = par_resultDiagnosticMessage;
        }
    }

    // кодовый язык текста
    public struct structCodePage
    {
        // номер кодовой страницы
		public int kodStranicaNomer; 
        // имя кодовой страницы
		public string kodStranicaName; 

        public structCodePage(int par_kodStranicaNomer, string par_kodStranicaName)
        {
            this.kodStranicaNomer = par_kodStranicaNomer;
            this.kodStranicaName = par_kodStranicaName;
        }
    }

    #endregion

    // класс для работы с портом и его настройками 
	// настройки переменные хранятся в Properties.Settings.Default
    static class OUnit // Options Unit
    {
        
        #region делегаты, переменные, константы и простейшие методы их получения

        #region serial-порт основные настройки
        // SerialPort, с которым работаем
		public static SerialPort sPort; 

        // скорость serial-порта:
        public static Dictionary<int, int> serialSpeed = new Dictionary<int, int>();
        public static int serialPort_Speed
        {
            get
            {
                try
                {
                    return serialSpeed[Properties.Settings.Default.indexComPort_Speed];
                }
                catch
                {
                    return serialSpeed[Properties.Settings.Default.indexComPort_SpeedDefault];
                }
            }
        }

        // число битов данных serial-порта
        public static Dictionary<int, int> serialDataBits = new Dictionary<int, int>();
        public static int serialPort_Databits//  = 8;
        {
            get
            {
                try
                {
                    return serialDataBits[Properties.Settings.Default.indexComPort_Databits];
                }
                catch
                {
                    return serialDataBits[Properties.Settings.Default.indexComPort_DatabitsDefault];
                }
            }
        }

        // бит чётности serial-порта 
		// допустимые значения: Even, Odd, None, Mark, Space
        public static Dictionary<int, Parity> serialParity = new Dictionary<int, Parity>();
        public static Parity serialPort_Parity // = Parity.None
        {
            get
            {
                try
                {
                    return serialParity[Properties.Settings.Default.indexComPort_Parity];
                }
                catch
                {
                    return serialParity[Properties.Settings.Default.indexComPort_ParityDefault];
                }
            }
        }

        // число стоповых битов serial-порта // допустимые значения: 1, 1.5, 2
        public static Dictionary<int, StopBits> serialStopBits = new Dictionary<int, StopBits>();
        public static StopBits serialPort_StopBits // = StopBits.Two;
        {
            get
            {
                try
                {
                    return serialStopBits[Properties.Settings.Default.indexComPort_StopBits];
                }
                catch
                {
                    return serialStopBits[Properties.Settings.Default.indexComPort_StopBitsDefault];
                }
            }
        }

        // открыт ли serial-порт
        public static Boolean isOpenSerialPort
        {
            get
            {
                if (sPort == null)
                    return (false);
                return (sPort.IsOpen);
            }
        }

        #endregion //  serial-порт основные настройки

        #region остальные настройки

        // делегат по передаче принятых данных
        public delegate void EventAnswerMsg(structPacketBytes data);
        // событие на приход пакета
        public static EventAnswerMsg HandlerEventAnswerMsg;

        // работает ли порт
        private static bool isPortWork = false; // для корерктного завершения работы порта

        // максимально допустимое количество пакетов, после которого последние выбрасываются
        private const int maxPacketBytesCount = 1000;
        public static int getMaxPacketBytesCount { get { return (maxPacketBytesCount); } }

        // отправленные данные в порт
        public static Dictionary<DateTime, structPacketBytes> dataCommand = new Dictionary<DateTime, structPacketBytes>();
        // принятые данные из порта
        public static Dictionary<DateTime, structPacketBytes> dataAnswer = new Dictionary<DateTime, structPacketBytes>();

        // поток чтения данных из порта
        private static Thread threadPortWatcher;

        // кодовые страницы
        public static Dictionary<int, structCodePage> codePage = new Dictionary<int, structCodePage>();
        public static structCodePage getCodePage
        {
            get
            {
                try
                {
                    return (codePage[Properties.Settings.Default.indexKodStranicaNomer]);
                }
                catch
                {
                    return (codePage[Properties.Settings.Default.indexKodStranicaNomerDefault]);
                }
            }
        }
        public static structCodePage GetCodePageByIndex(int index)
        {
            try
            {
                return (codePage[index]);
            }
            catch
            {
                return (codePage[Properties.Settings.Default.indexKodStranicaNomerDefault]);
            }
        }

        // формат времени
        public static Dictionary<int, string> timeFormat = new Dictionary<int, string>();

        // макс. кол-во байт в каждой строке данных
        public static Dictionary<int, int> kolBytesPerLine = new Dictionary<int, int>();        
        public static int getKolBytesPerLine
        {
            get
            {
                try
                {
                    return kolBytesPerLine[Properties.Settings.Default.kolBytesPerLineIndex];
                }
                catch
                {
                    return kolBytesPerLine[Properties.Settings.Default.kolBytesPerLineIndexDefault];
                }
            }
        }
        public static int GetKolBytesPerLineByIndex(int index)
        {
            try
            {
                return kolBytesPerLine[index];
            }
            catch
            {
                return kolBytesPerLine[Properties.Settings.Default.kolBytesPerLineIndexDefault];
            }
        }

        // разделитель байтов
        public static Dictionary<int, string> razdBytes = new Dictionary<int, string>();
        public static char getRazdByteChar 
        { 
            get 
            {
                try
                {
                    return (razdBytes[Properties.Settings.Default.razdByteIndex][0]);
                }
                catch
                {
                    return (razdBytes[Properties.Settings.Default.razdByteIndexDefault][0]);
                }
            } 
        }
        public static char GetRazdByteCharByIndex(int i) 
        {
            try
            {
                return (razdBytes[i][0]);
            }
            catch
            {
                return (razdBytes[Properties.Settings.Default.razdByteIndexDefault][0]);
            }
        }

        // буфер принятых данных
        private static structPacketBytes bytesReceived;
        // для блокировки принятых данных        
		private static object lockerForBytesReceived = new object();
        // считать буфер принятых данных и очистить его
		public static structPacketBytes getBytesReceived 
        {
            get
            {
                lock (lockerForBytesReceived)
                    try
                    {
                        bytesReceived.timeDone = DateTime.Now;
                        bytesReceived.isResultError = false;
                        bytesReceived.resultDiagnosticMessage = "Пакет принят успешно";
                        return (bytesReceived);
                    }
                    finally
                    {                        
                        dataAnswer.Add(bytesReceived.timeDone, bytesReceived); // сохраняем  
                        while (dataAnswer.Count > maxPacketBytesCount)
                            dataAnswer.Remove(dataAnswer.Keys.ElementAt(0)); 
                        bytesReceived.dataBytes = new byte[0];
                        bytesReceived.timeDone = DateTime.Now;
                        bytesReceived.isResultError = true;
                        bytesReceived.resultDiagnosticMessage = "Пакет ещё не принят";                        
                    }
            }
        }
		
        // добавить байт к буферу
        private static void AddByteToBytesReceived(byte val)
        {
            lock (lockerForBytesReceived)
            {
                bytesReceived.dataBytes = Assist.AddByteToBytes(val, bytesReceived.dataBytes);
            }
        }

        // максимально возможное количество символов hex-текста в консоли // учитываем число байт и разделитель |
        public static int maxLengthConsolHexText
        {
            get
            {
                int res = (getKolBytesPerLine - 1) * 3 + 2;
                if (Properties.Settings.Default.isRazdHex8)
                    res += ((int)Math.Ceiling((double)getKolBytesPerLine / 8) * 2); //  + "| " -> +2символа
                return (res);
            }
        }

        #endregion // остальные настройки

        #endregion // делегаты, переменные, константы и простые методы для их получения

        #region методы hard

        #region serial-порт работа с портом

        // поток слежения за буфером
        private static void PortWatcher()
        {
            DateTime dt_last = DateTime.Now; // время последнего принятого байта      
            bytesReceived.dataBytes = new byte[0]; // т.к. в структуре не могут содержаться инициализаторы полей экземпляров

            while (isPortWork)
            {
                if (sPort.BytesToRead != 0)
                {
                    dt_last = DateTime.Now;
                    AddByteToBytesReceived((byte)sPort.ReadByte());
                }
                if (bytesReceived.dataBytes.Count() > 0) // если не первый символ, оцениваем время
                    if (DateTime.Now - dt_last > // если пауза превысила допустимое, это конец пакета
                        TimeSpan.FromMilliseconds(Properties.Settings.Default.serialPort_WaitAnswer))
                    {
                        HandlerEventAnswerMsg.BeginInvoke(getBytesReceived, null, null);
                    }
            }
        }

        /*
        // событие на приход пакета
        private static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
        {
            try
            {
//                 string dat = sPort.ReadLine();
  //               HandlerReceiveDataFromSerialPort.BeginInvoke(dat, null, null);
            SerialPort sp = (SerialPort)sender;
            // принятый пакет
            byte[] receiveData = new byte[0];// byte[sp.BytesToRead];
                while (sp.BytesToRead != 0) // всегда проходим дальше, если только нет что принимать
                {
                    receiveData = Assist.AddByteToBytes((byte)sp.ReadByte(), receiveData);
                    if (sp.BytesToRead == 0)
                        Thread.Sleep( (int)Math.Ceiling((double)1000 / ComPort_Speed)); // на всякий случай, ждём один такт скорости порта                    
                    if (sp.BytesToRead == 0) // NetFramework ждёт только до появления первого символа, поэтому, если так и не пришёл, то ждём оставшуюся часть
                        if (Properties.Settings.Default.isSerialPort_WaitAnswer)
                            Thread.Sleep(Properties.Settings.Default.serialPort_WaitAnswer - (int)(2*1000 / ComPort_Speed));
                }
                if (receiveData.Count() > 0)
                    HandlerEventAnswerMsg(receiveData);
            }
            catch
            {
                // HandlerEventAnswerMsg(receiveData);
            }
        }
        */
        // закрыть порт
        public static Boolean CloseSerialPort() { string s = ""; return (CloseSerialPort(ref s)); }
        public static Boolean CloseSerialPort(ref string result_message)
        {
            if (sPort == null) return (true);

            try
            {
                // останавливаем приём данных
                // sPort.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
                isPortWork = false;
                threadPortWatcher.Join();
                sPort.DiscardInBuffer();
                sPort.DiscardOutBuffer();
                sPort.Close(); // может зависнуть если использовать sPort.DataReceived
                sPort = null;
            }
            catch (Exception ex)
            {
                result_message = "Ошибка закрытия serial-порта: " + ex.Message;
                result_message = ex.Message;//  "Error. Serial Port is bad or missing";
                sPort = null;
                return (false);
            }
            return (true);
        }

        // открыть порт
        public static Boolean OpenSerialPort() { string s = ""; return (OpenSerialPort(Properties.Settings.Default.nameComPort, ref s)); }
        public static Boolean OpenSerialPort(string portName) { string s = ""; return (OpenSerialPort(portName, ref s)); }
        public static Boolean OpenSerialPort(ref string result_message) { return (OpenSerialPort(Properties.Settings.Default.nameComPort, ref result_message)); }
        public static Boolean OpenSerialPort(string portName, ref string result_message)
        {
            try
            {
                result_message = "Serial-порт недоступен или неверные параметры.";
                if (sPort == null)
                {
                    // основные настройки
                    if (serialPort_StopBits == StopBits.None) // при использовании System.IO.Ports.StopBits.None генерируется исключение System.ArgumentOutOfRangeException.
                        sPort = new SerialPort(portName,
                                               serialPort_Speed,
                                               serialPort_Parity,
                                               serialPort_Databits);
                    else
                        sPort = new SerialPort(portName,
                                               serialPort_Speed,
                                               serialPort_Parity,
                                               serialPort_Databits,
                                               serialPort_StopBits);
                    sPort.Handshake = Handshake.None;
                    // время ожидания записи
                    if (Properties.Settings.Default.isSerialPort_WaitWrite)
                        sPort.WriteTimeout = SerialPort.InfiniteTimeout;
                    else
                        sPort.WriteTimeout = Properties.Settings.Default.serialPort_WaitWrite;
                    // время ожидания чтения
                    if (Properties.Settings.Default.isSerialPort_WaitAnswer)
                        sPort.ReadTimeout = SerialPort.InfiniteTimeout;
                    else
                        sPort.ReadTimeout = Properties.Settings.Default.serialPort_WaitAnswer;
                    // задаём событие на приход данных
                    // sPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); может зависнуть если использовать sPort.DataReceived
                } // if (sPort == null)
                else // 
                {
                    if (sPort.IsOpen)
                    {
                        result_message = "Serial-порт уже открыт";
                        return (true);
                    }
                    else
                    {
                        CloseSerialPort(ref result_message);
                        return OpenSerialPort(portName, ref result_message);
                    }
                }
                try
                {
                    sPort.Open();
                    sPort.DiscardInBuffer();
                    sPort.DiscardOutBuffer();
                    isPortWork = true;
                    threadPortWatcher = new Thread(PortWatcher);
                    threadPortWatcher.Start();
                    result_message = "Serial-порт открыт успешно";
                    return (true);
                }
                catch (Exception ex)
                {
                    result_message += "\r\nСообщение системы: " + ex.Message;
                    return (false);
                }
            }
            finally
            {
            }
        }

        // протестировать порт
        public static Boolean TestSerialPort() { return (TestSerialPort(Properties.Settings.Default.nameComPort)); }
        public static Boolean TestSerialPort(string portName)
        {
            if (isPortWork) // если идёт работа с портом, значит ok
                return (true);
            bool res = OpenSerialPort(portName);
            CloseSerialPort();
            return (res);
        }

        // запись пакета в порт
        public static Boolean WriteData(ref structPacketBytes structPaketSend)
        {
            structPaketSend.isResultError = true;
            structPaketSend.timeDone = DateTime.Now;
            if (sPort == null)
            {
                structPaketSend.resultDiagnosticMessage = "Serial Port закрыт или недоступен.";
                return (false);
            }
            try
            {
                try
                {
                    if (sPort.IsOpen)
                    {
                        structPaketSend.resultDiagnosticMessage = "Ошибка при записи в serial-порт";
                        sPort.Write(structPaketSend.dataBytes, 0, structPaketSend.dataBytes.Length);
                        structPaketSend.resultDiagnosticMessage = "Данные в serial-порт записаны успешно";
                        structPaketSend.isResultError = false;
                        return (true);
                    }
                    else
                    {
                        structPaketSend.resultDiagnosticMessage = "Serial Port закрыт или недоступен.";
                        return (false);
                    }

                }
                catch (Exception ex)
                {
                    structPaketSend.resultDiagnosticMessage += ex.Message;
                    return (false);
                }
            }
            finally
            {
                dataCommand.Add(structPaketSend.timeDone, structPaketSend); // сохраняем
                while (dataCommand.Count > maxPacketBytesCount)
                    dataCommand.Remove(dataCommand.Keys.ElementAt(0));
            }
        }

        #endregion // serial-порт работа с портом

        #region методы инициализация и сохранение настроек

        // инициализация настроек // достраиваём структуру данных
        public static bool OptionsInit()
        {

            #region serialport
            // скорость
            serialSpeed.Add(0, 300);
            serialSpeed.Add(1, 600);
            serialSpeed.Add(2, 1200);
            serialSpeed.Add(3, 2400);
            serialSpeed.Add(4, 4800);
            serialSpeed.Add(5, 9600);
            serialSpeed.Add(6, 19200);
            serialSpeed.Add(7, 31250);
            serialSpeed.Add(8, 38400);
            serialSpeed.Add(9, 57600);
            serialSpeed.Add(10, 115200);

            // бит данных            
            serialDataBits.Add(0, 5);
            serialDataBits.Add(1, 6);
            serialDataBits.Add(2, 7);
            serialDataBits.Add(3, 8);

            // бит чётности
            serialParity.Add(0, Parity.None);
            serialParity.Add(1, Parity.Odd);
            serialParity.Add(2, Parity.Even);
            serialParity.Add(3, Parity.Mark);
            serialParity.Add(4, Parity.Space);

            // стоповые биты
            serialStopBits.Add(0, StopBits.None);
            serialStopBits.Add(1, StopBits.One);
            serialStopBits.Add(2, StopBits.OnePointFive);
            serialStopBits.Add(3, StopBits.Two);

            if (!(serialSpeed.Keys.Contains(Properties.Settings.Default.indexComPort_Speed)))
                Properties.Settings.Default.indexComPort_Speed = Properties.Settings.Default.indexComPort_SpeedDefault;

            if (!(serialDataBits.Keys.Contains(Properties.Settings.Default.indexComPort_Databits)))
                Properties.Settings.Default.indexComPort_Databits = Properties.Settings.Default.indexComPort_DatabitsDefault;

            if (!(serialParity.Keys.Contains(Properties.Settings.Default.indexComPort_Parity)))
                Properties.Settings.Default.indexComPort_Parity = Properties.Settings.Default.indexComPort_ParityDefault;

            if (!(serialStopBits.Keys.Contains(Properties.Settings.Default.indexComPort_StopBits)))
                Properties.Settings.Default.indexComPort_StopBits = Properties.Settings.Default.indexComPort_StopBitsDefault;
            #endregion

            #region папки и файлы
            try
            {
                // создаём папку для создания файлов программы
                Properties.Settings.Default.folderForDocDefault = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Тестер порта";
                if (!Directory.Exists(Properties.Settings.Default.folderForDocDefault))
                    Directory.CreateDirectory(Properties.Settings.Default.folderForDocDefault);
            }
            catch
            {
                Properties.Settings.Default.folderForDocDefault = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            try
            {
                // имя файла для сохранения логов ответов по умолчанию
                Properties.Settings.Default.fileAnswerDefault = Properties.Settings.Default.folderForDocDefault + "\\Принятые данные с порта.txt";
                if (!Directory.Exists(Path.GetDirectoryName(Properties.Settings.Default.fileAnswer)))
                    Directory.CreateDirectory(Path.GetDirectoryName(Properties.Settings.Default.fileAnswer));
            }
            catch
            {
                Properties.Settings.Default.fileAnswer = Properties.Settings.Default.fileAnswerDefault;
            }

            try
            {
                // имя файла для сохранения логов отправленных данных по умолчанию
                Properties.Settings.Default.fileCommandDefault = Properties.Settings.Default.folderForDocDefault + "\\Отправленные данные в порт.txt";
                if (!Directory.Exists(Path.GetDirectoryName(Properties.Settings.Default.fileAnswer)))
                    Directory.CreateDirectory(Path.GetDirectoryName(Properties.Settings.Default.fileAnswer));
            }
            catch
            {
                Properties.Settings.Default.fileCommand = Properties.Settings.Default.fileCommandDefault;
            }
            #endregion

            #region кодовые страницы
            // https:// msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx
            int index = 0;
            codePage.Add(index++, new structCodePage(37, "IBM037"));
            codePage.Add(index++, new structCodePage(437, "IBM437"));
            codePage.Add(index++, new structCodePage(500, "IBM500"));
            codePage.Add(index++, new structCodePage(708, "ASMO 708"));
            codePage.Add(index++, new structCodePage(720, "DOS 720"));
            codePage.Add(index++, new structCodePage(737, "ibm737"));
            codePage.Add(index++, new structCodePage(775, "ibm775"));
            codePage.Add(index++, new structCodePage(850, "ibm850"));
            codePage.Add(index++, new structCodePage(852, "ibm852"));
            codePage.Add(index++, new structCodePage(855, "IBM855"));
            codePage.Add(index++, new structCodePage(857, "ibm857"));
            codePage.Add(index++, new structCodePage(858, "IBM00858"));
            codePage.Add(index++, new structCodePage(860, "IBM860"));
            codePage.Add(index++, new structCodePage(861, "ibm861"));
            codePage.Add(index++, new structCodePage(862, "DOS-862"));
            codePage.Add(index++, new structCodePage(863, "IBM863"));
            codePage.Add(index++, new structCodePage(864, "IBM864"));
            codePage.Add(index++, new structCodePage(865, "IBM865"));
            codePage.Add(index++, new structCodePage(866, "cp866"));
            codePage.Add(index++, new structCodePage(869, "ibm869"));
            codePage.Add(index++, new structCodePage(870, "IBM870"));
            codePage.Add(index++, new structCodePage(874, "Windows-874"));
            codePage.Add(index++, new structCodePage(875, "cp875"));
            codePage.Add(index++, new structCodePage(932, "shift_jis"));
            codePage.Add(index++, new structCodePage(936, "GB2312"));
            codePage.Add(index++, new structCodePage(949, "ks_c_5601 1987"));
            codePage.Add(index++, new structCodePage(950, "Big5"));
            codePage.Add(index++, new structCodePage(1026, "IBM1026"));
            codePage.Add(index++, new structCodePage(1047, "IBM01047"));
            codePage.Add(index++, new structCodePage(1140, "IBM01140"));
            codePage.Add(index++, new structCodePage(1141, "IBM01141"));
            codePage.Add(index++, new structCodePage(1142, "IBM01142"));
            codePage.Add(index++, new structCodePage(1143, "IBM01143"));
            codePage.Add(index++, new structCodePage(1144, "IBM01144"));
            codePage.Add(index++, new structCodePage(1145, "IBM01145"));
            codePage.Add(index++, new structCodePage(1146, "IBM01146"));
            codePage.Add(index++, new structCodePage(1147, "IBM01147"));
            codePage.Add(index++, new structCodePage(1148, "IBM01148"));
            codePage.Add(index++, new structCodePage(1149, "IBM01149"));
            codePage.Add(index++, new structCodePage(1200, "UTF-16"));
            codePage.Add(index++, new structCodePage(1201, "unicodeFFFE"));
            codePage.Add(index++, new structCodePage(1250, "Windows-1250"));
            codePage.Add(index++, new structCodePage(1251, "Windows-1251"));
            codePage.Add(index++, new structCodePage(1252, "Windows -1252"));
            codePage.Add(index++, new structCodePage(1253, "Windows-1253"));
            codePage.Add(index++, new structCodePage(1254, "Windows-1254"));
            codePage.Add(index++, new structCodePage(1255, "Windows-1255"));
            codePage.Add(index++, new structCodePage(1256, "Windows-1256"));
            codePage.Add(index++, new structCodePage(1257, "Windows-1257"));
            codePage.Add(index++, new structCodePage(1258, "Windows-1258"));
            codePage.Add(index++, new structCodePage(1361, "Джохаб"));
            codePage.Add(index++, new structCodePage(10000, "Macintosh"));
            codePage.Add(index++, new structCodePage(10001, "x mac японского языка."));
            codePage.Add(index++, new structCodePage(10002, "x-mac-chinesetrad"));
            codePage.Add(index++, new structCodePage(10003, "x mac корейского языка."));
            codePage.Add(index++, new structCodePage(10004, "x mac арабском языке."));
            codePage.Add(index++, new structCodePage(10005, "x mac иврита"));
            codePage.Add(index++, new structCodePage(10006, "x-mac греческая"));
            codePage.Add(index++, new structCodePage(10007, "x (mac-кириллица)"));
            codePage.Add(index++, new structCodePage(10008, "x-mac-chinesesimp"));
            codePage.Add(index++, new structCodePage(10010, "x-mac румынский"));
            codePage.Add(index++, new structCodePage(10017, "x-mac украинский"));
            codePage.Add(index++, new structCodePage(10021, "x mac тайском языке."));
            codePage.Add(index++, new structCodePage(10029, "x-mac-ce"));
            codePage.Add(index++, new structCodePage(10079, "x-mac исландская"));
            codePage.Add(index++, new structCodePage(10081, "x mac турецкого языка."));
            codePage.Add(index++, new structCodePage(10082, "x-mac Хорватский"));
            codePage.Add(index++, new structCodePage(12000, "UTF-32"));
            codePage.Add(index++, new structCodePage(12001, "UTF-32BE"));
            codePage.Add(index++, new structCodePage(20000, "x китайский общих ИМЕН."));
            codePage.Add(index++, new structCodePage(20001, "x cp20001"));
            codePage.Add(index++, new structCodePage(20002, "x китайский Eten"));
            codePage.Add(index++, new structCodePage(20003, "x cp20003"));
            codePage.Add(index++, new structCodePage(20004, "x cp20004"));
            codePage.Add(index++, new structCodePage(20005, "x cp20005"));
            codePage.Add(index++, new structCodePage(20105, "x IA5"));
            codePage.Add(index++, new structCodePage(20106, "x IA5 немецкого языка."));
            codePage.Add(index++, new structCodePage(20107, "x-IA5 — шведский"));
            codePage.Add(index++, new structCodePage(20108, "x-IA5-норвежский"));
            codePage.Add(index++, new structCodePage(20127, "US-ascii"));
            codePage.Add(index++, new structCodePage(20261, "x cp20261"));
            codePage.Add(index++, new structCodePage(20269, "x cp20269"));
            codePage.Add(index++, new structCodePage(20273, "IBM273"));
            codePage.Add(index++, new structCodePage(20277, "IBM277"));
            codePage.Add(index++, new structCodePage(20278, "IBM278"));
            codePage.Add(index++, new structCodePage(20280, "IBM280"));
            codePage.Add(index++, new structCodePage(20284, "IBM284"));
            codePage.Add(index++, new structCodePage(20285, "IBM285"));
            codePage.Add(index++, new structCodePage(20290, "IBM290"));
            codePage.Add(index++, new structCodePage(20297, "IBM297"));
            codePage.Add(index++, new structCodePage(20420, "IBM420"));
            codePage.Add(index++, new structCodePage(20423, "IBM423"));
            codePage.Add(index++, new structCodePage(20424, "IBM424"));
            codePage.Add(index++, new structCodePage(20833, "x-EBCDIC-KoreanExtended"));
            codePage.Add(index++, new structCodePage(20838, "Тайский IBM"));
            codePage.Add(index++, new structCodePage(20866, "КОИ8 r"));
            codePage.Add(index++, new structCodePage(20871, "IBM871"));
            codePage.Add(index++, new structCodePage(20880, "IBM880"));
            codePage.Add(index++, new structCodePage(20905, "IBM905"));
            codePage.Add(index++, new structCodePage(20924, "IBM00924"));
            codePage.Add(index++, new structCodePage(20932, "EUC-JP"));
            codePage.Add(index++, new structCodePage(20936, "x cp20936"));
            codePage.Add(index++, new structCodePage(20949, "x cp20949"));
            codePage.Add(index++, new structCodePage(21025, "cp1025"));
            codePage.Add(index++, new structCodePage(21866, "КОИ8 u"));
            codePage.Add(index++, new structCodePage(28591, "ISO-8859-1"));
            codePage.Add(index++, new structCodePage(28592, "ISO-8859-2"));
            codePage.Add(index++, new structCodePage(28593, "ISO-8859-3"));
            codePage.Add(index++, new structCodePage(28594, "ISO-8859-4"));
            codePage.Add(index++, new structCodePage(28595, "ISO-8859-5"));
            codePage.Add(index++, new structCodePage(28596, "ISO-8859-6"));
            codePage.Add(index++, new structCodePage(28597, "ISO-8859-7"));
            codePage.Add(index++, new structCodePage(28598, "ISO-8859-8"));
            codePage.Add(index++, new structCodePage(28599, "ISO-8859-9"));
            codePage.Add(index++, new structCodePage(28603, "ISO-8859-13"));
            codePage.Add(index++, new structCodePage(28605, "ISO-8859-15"));
            codePage.Add(index++, new structCodePage(29001, "x Europa"));
            codePage.Add(index++, new structCodePage(38598, "ISO-8859-8-i"));
            codePage.Add(index++, new structCodePage(50220, "ISO-2022-jp"));
            codePage.Add(index++, new structCodePage(50221, "csISO2022JP"));
            codePage.Add(index++, new structCodePage(50222, "ISO-2022-jp"));
            codePage.Add(index++, new structCodePage(50225, "ISO-2022-kr"));
            codePage.Add(index++, new structCodePage(50227, "x cp50227"));
            codePage.Add(index++, new structCodePage(51932, "EUC-jp"));
            codePage.Add(index++, new structCodePage(51936, "EUC CN"));
            codePage.Add(index++, new structCodePage(51949, "EUC-kr"));
            codePage.Add(index++, new structCodePage(52936, "Гц gb-2312"));
            codePage.Add(index++, new structCodePage(54936, "GB18030"));
            codePage.Add(index++, new structCodePage(57002, "x-iscii-de."));
            codePage.Add(index++, new structCodePage(57003, "x iscii быть"));
            codePage.Add(index++, new structCodePage(57004, "x-iscii-ta"));
            codePage.Add(index++, new structCodePage(57005, "x-iscii-te"));
            codePage.Add(index++, new structCodePage(57006, "x iscii как"));
            codePage.Add(index++, new structCodePage(57007, "или x iscii"));
            codePage.Add(index++, new structCodePage(57008, "x-iscii ка"));
            codePage.Add(index++, new structCodePage(57009, "x iscii скользящее среднее."));
            codePage.Add(index++, new structCodePage(57010, "x iscii графического интерфейс."));
            codePage.Add(index++, new structCodePage(57011, "x-iscii-pa"));
            codePage.Add(index++, new structCodePage(65000, "UTF-7"));
            codePage.Add(index++, new structCodePage(65001, "UTF-8"));
            #endregion

            #region остальное
            // формат времени // ключ соответствует индексу выбранного в chechbox пункта
            timeFormat.Add(0, "дд:ММ:гг чч:мм:сс");
            timeFormat.Add(1, "дд:ММ чч:мм:сс");
            timeFormat.Add(2, "дд чч:мм:сс");
            timeFormat.Add(3, "чч:мм:сс");
            timeFormat.Add(4, "мм:сс");
            timeFormat.Add(5, "сс");

            // макс. кол-во байт в каждой строке данных // ключ соответствует индексу выбранного в chechbox пункта
            kolBytesPerLine.Add(0, 8);
            kolBytesPerLine.Add(1, 16);
            kolBytesPerLine.Add(2, 32);
            kolBytesPerLine.Add(3, 64);
            kolBytesPerLine.Add(4, 128);
            kolBytesPerLine.Add(5, 256);
            kolBytesPerLine.Add(6, 512);
            kolBytesPerLine.Add(7, 1024);

            // разделитель байтов // ключ соответствует индексу выбранного в chechbox пункта
            razdBytes.Add(0, "  [пробел]");
            razdBytes.Add(1, "_ [подчёркивание]");
            razdBytes.Add(2, ". [точка]");
            razdBytes.Add(3, ", [запятая]");
            razdBytes.Add(4, "- [дефис]");
            razdBytes.Add(5, "* [звёздочка");
            razdBytes.Add(6, ": [двоеточие]");
            razdBytes.Add(7, "; [точка с запятой]");
            #endregion

            return (true);
        }

        // сохранение настроек
        public static bool SaveOptions()
        {
            Properties.Settings.Default.Save();
            return (true);
        }

        #endregion // инициализация и сохранение настроек

        #endregion // методы hard
    }
}
