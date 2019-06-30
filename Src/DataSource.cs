using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace Dashboard
{
    public class DataSource
    {

        public static ConcurrentQueue<Object> RTUpdatesQueue = new ConcurrentQueue<Object>();

        public static ConcurrentQueue<Security> WatchedTickers = new ConcurrentQueue<Security>();

        public static ConcurrentDictionary<string, double> RTPricesQueue = new ConcurrentDictionary<string, double>();
        public static void StartRTWatch()
        {
            // watch OMS
            Action watch_oms = () =>
            {
                //Mahmoud add OMS start here
                /*
                 start the callback which should enqueue new/updated position into "RTUpdatesQueue"
                  */
                /* 
                 20190417|MSEUR|AFX GY|118380.444|337.527749999996|16.946999999999|320.580749999997|0|80.6|80.55|1300|300|1000|80.31625|0|0|1|E|EUR|4562.39050999997|4562.39050999997|4562.39050999997
20190417|MSEUR|ALC SW|0|0|0|0|0|57.85|56.3|0|0|0|0|0|0|1|E|CHF|22308.0728505001|22308.0728505001|22308.0728505001
20190417|MSEUR|AZN LN|0|0|-0|0|0|5914|6015|0|0|0|0|0|0|1|E|GBp|-919.232414999996|-919.232414999996|-919.232414999996
                 
                 */
                Random rand = new Random();
                while (true)
                {
                    System.Threading.Thread.Sleep(5500);
                    Position pos = new Position()
                    {
                        PortfolioName = "MSEUR",
                        Currency = "USD",
                        UnderlyingType = "E",
                        UnderlyingTicker = "AFX GY",
                        Underlying = "AFX GY",

                        RealizedPnL = Convert.ToDouble(337.527749999996),
                        BODPnL = Convert.ToDouble(16.946999999999),
                        BeginOfDayQuantity = Convert.ToDouble(6000),
                        BoughtQuantity = Convert.ToDouble(0),
                        BoughtAveragePrice = Convert.ToDouble(12.3 * rand.NextDouble()),
                        SoldQuantity = Convert.ToDouble(50* rand.NextDouble()),
                        SoldAveragePrice = Convert.ToDouble(19.5* rand.NextDouble()),
                        YTDPnL = Convert.ToDouble(15000.58),
                        MTDPnL = Convert.ToDouble(5000.69),
                        WTDPnL = Convert.ToDouble(32000.365)
                    };
                 
                    DataSource.RTUpdatesQueue.Enqueue(pos);

                    System.Threading.Thread.Sleep(1500);
                    //another one
                    Position pos2 = new Position()
                    {
                        PortfolioName = "MSEUR",
                        Currency = "EUR",
                        UnderlyingType = "E",
                        UnderlyingTicker = "ALC SW",
                        Underlying = "ALC SW",

                        RealizedPnL = Convert.ToDouble(337.527749999996),
                        BODPnL = Convert.ToDouble(16.946999999999),
                        BeginOfDayQuantity = Convert.ToDouble(6000),
                        BoughtQuantity = Convert.ToDouble(0),
                        BoughtAveragePrice = Convert.ToDouble(12.3 * rand.NextDouble()),
                        SoldQuantity = Convert.ToDouble(50 * rand.NextDouble()),
                        SoldAveragePrice = Convert.ToDouble(19.5 * rand.NextDouble()),
                        YTDPnL = Convert.ToDouble(15000.58),
                        MTDPnL = Convert.ToDouble(5000.69),
                        WTDPnL = Convert.ToDouble(32000.365)
                    };
                
                    DataSource.RTUpdatesQueue.Enqueue(pos2);
                }
            };

            //    // watch bloom this action is starting at the start of the program
            Action watch_bloomberg = () =>
            {
                log4net.ILog Log =
                   log4net.LogManager.GetLogger("DataSource.StartWatch.watch_bloomberg");// System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
                /*
                 * //Mahmoud 
                 * all the securities for bloom to deal with for both RT ans static data 
                 * are going to be in the "WatchedTickers" queue as a Security object
                 * (The push in the queue will be done from the portfolio form side (once there a is a new security 
                 * coming from OMS in a new position))
                 * So I suggest you do the following:
                 * 1- start an empty subscription to bloomberg
                 * 2- whithin a while(true) keep waiting anything coming in the queue
                 * 3- once you have a security in the queue 1) get its static data and push in the RT updates then 2) add it to the subscription for RT.
                 */
                // some how pull oms system and enqueue into MarketDataUpdates
                int mult = 1;
                Random rand = new Random();
                while (true)
                {
                    
                    
                    while (false == WatchedTickers.IsEmpty)
                    {
                        Security sec = new Security("");
                        WatchedTickers.TryDequeue(out sec);
                     
                        sec.Currency = "EUR";
                        sec.Country = "France ";
                        if(mult % 2 == 0)
                            sec.Country += Convert.ToString(mult);
                        sec.QuotationFactor = 1;
                        sec.Multiplier = 1;
                        if(mult % 6 == 0)
                            sec.Multiplier = 100;
                        sec.Sector = "Sector 1" + Convert.ToString(mult);

                        sec.PreviousAdjClose = 5;
                        if (sec.Name == "AFX GY")
                            sec.PreviousAdjClose =15.5;
                        else if (sec.Name == "ALC SW")
                            sec.PreviousAdjClose = 19.5;

                        sec.PreviousClose = 4;
                        if (sec.Name == "AFX GY")
                            sec.PreviousClose = 15.5-5;
                        else if (sec.Name == "ALC SW")
                            sec.PreviousClose = 19.5-5;

                        RTUpdatesQueue.Enqueue(sec);
                        mult++;
                    }
                    System.Threading.Thread.Sleep(3500);
                    Log.Info("waiting prices..");
                    PriceUpdate update_ticker = new PriceUpdate("AFX GY", mult * rand.NextDouble());

                    RTUpdatesQueue.Enqueue(update_ticker);
                    PriceUpdate update_ticker2 = new PriceUpdate("ALC SW", mult * rand.NextDouble());

                    RTUpdatesQueue.Enqueue(update_ticker2);

                    PriceUpdate update_ticker3 = new PriceUpdate("AZN LN", mult * rand.NextDouble());

                    RTUpdatesQueue.Enqueue(update_ticker3);

                    PriceUpdate update_ticker4 = new PriceUpdate("BAER SW", mult * rand.NextDouble());

                    RTUpdatesQueue.Enqueue(update_ticker4);

                    PriceUpdate update_ticker5 = new PriceUpdate("DNBF US", mult * rand.NextDouble());

                    RTUpdatesQueue.Enqueue(update_ticker5);
                  if(mult % 15 == 0)
                    { 
                    PriceUpdate update_ticker_fx = new PriceUpdate("EUR/USD",  mult * rand.NextDouble());
                    RTUpdatesQueue.Enqueue(update_ticker_fx);
                    }

                }

            };
            Task.Factory.StartNew(() =>
            {
                Parallel.Invoke(watch_bloomberg, watch_oms);
            });
        }
    }
}