using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using utility;



namespace _113screenshot
{
    internal class Program
    {
        static myinclude my = new myinclude();
        static async Task Main(string[] args)
        {
            string VERSION = "0.01";


            bool showHelp = false;
            string url = "";
            int delayms = 3000;
            int w = 1920;
            int h = 1080;

            string output = "output.png"; // 默認輸出文件名        
            // 解析命令行參數
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-?":
                    case "-help":
                        showHelp = true;
                        break;
                    case "-url":
                        url = args[++i];
                        break;
                    case "-d":
                    case "-delay":
                        delayms = Convert.ToInt32(args[++i]);
                        break;
                    case "-w":
                    case "-width":
                        w = Convert.ToInt32(args[++i]);
                        break;
                    case "-h":
                    case "-height":
                        h = Convert.ToInt32(args[++i]);
                        break;
                    case "-o":
                    case "-output":
                        output = args[++i];
                        break;
                }
            }

            if (showHelp || string.IsNullOrEmpty(url) || string.IsNullOrEmpty(output))
            {
                Console.WriteLine("My URL Screenshot");
                Console.WriteLine("Author: FeatherMountain (https://3wa.tw)");
                Console.WriteLine("Version: " + VERSION);
                Console.WriteLine("  Usage:");
                Console.WriteLine("   -help, -?   This helper.");
                Console.WriteLine("   -url        URL , or test.html");
                Console.WriteLine("   -width, -w  Screenshot width.");
                Console.WriteLine("   -height, -h Screenshot height.");
                Console.WriteLine("   -delay, -d  Delay N ms download. Default 3000.");
                Console.WriteLine("   -output, -o [Default: output.sqlite] The output SQLite file name.");
                Console.WriteLine("");
                Console.WriteLine(" Example:");
                Console.WriteLine("   113screenshot.exe -url test.html -w 1920 -h 1080 -d 3000 -o output.png");
                return;
            }



            // Download the Chromium browser if needed
            await new BrowserFetcher().DownloadAsync();

            // Launch the browser
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            try
            {
                // Create a new page
                var page = await browser.NewPageAsync();

                // Navigate to the URL
                //page.goto(`file:///C:/pup_scrapper/testpage/TM.html`);

                if (my.is_file(url))
                {
                    url = "file://" + System.IO.Path.GetFullPath(url).Replace("\\", "/");
                }

                await page.GoToAsync(url);

                // Set the viewport size if needed (optional)
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = w,
                    Height = h
                });
                Thread.Sleep(delayms);
                // Take the screenshot
                await page.ScreenshotAsync(output);

                // Close the browser
                await browser.CloseAsync();
                browser.Dispose();
                browser = null;
                Console.WriteLine("Screenshot saved! " + System.IO.Path.GetFullPath(output));
            }
            catch
            {
                browser.Dispose();
                browser = null;
            }
        }
    }
}
