using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAG_TeknikResimDamgalama
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            //Patagames.Pdf.dll ve Patagames.Pdf.Windforms.dll dosyalarını exe klasörünün
            //altındaki Pdfium.Net\net46 klasöründen okumasını isytiyorum. Bu nedenle
            //Solution Explorer'da References nodunda Patagames.Pdf ve Patagames.Pdf.Winforms
            //nodlarına tıklayıp Properties'de Copy Local'ı false yaptım. Böylece bu dll'ler
            //debug folderına exenin yanına otomatik kopyalanmıyor. Sonra Aşağıdaki satırı
            //ekledim. Bu satır her dll aradığında CurrentDomain_AssemblyResolve fonksiyonunu
            //çağırıyor. O fonksiyonda da dll'nin yerini gösteriyorum. Bunu yapmazsam, dll'ler
            //exe klasörüyle aynı klasörde olması gerekiyor. Ben exenin tek başına, gereken
            //diğer tüm dosyaların ayrı klasörlerde olmasını istediğim için bunu yaptım.
            System.AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var probingPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Pdfium.NET.SDK\net46";
            var assyName = new System.Reflection.AssemblyName(args.Name);

            var newPath = System.IO.Path.Combine(probingPath, assyName.Name);
            if (!newPath.EndsWith(".dll"))
            {
                newPath = newPath + ".dll";
            }
            if (System.IO.File.Exists(newPath))
            {
                var assy = System.Reflection.Assembly.LoadFile(newPath);
                return assy;
            }
            return null;
        }
    }
}
