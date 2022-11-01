using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHepler : IFileHelper
    {
        public void Delete(string filePath)  // Buradaki string filePath; "CarImageManager" dan gelen dosyanin kaydedildiği adres ve adı
        {
            if (File.Exists(filePath))  // Gelen adreste dosyanın olup olmadığını kontrol ediyoruz.
            {
                File.Delete(filePath);  // Eğer dosya var ise siliniyor.
            }
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            if (File.Exists(filePath))  // Gelen adreste dosyanın olup olmadığını kontrol ediyoruz.
            {
                File.Delete(filePath);  // Eğer dosya var ise siliniyor.
            }
            return Upload(file, root);  // Eski dosya silindikten sonra yerine geçecek yeni dosya için Upload metodu ile yeni dosyayı ekliyoruz.
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0)  // Dosyanın gönderildiği dosya uzunluğundan kontrol ediliyor.
            {
                if (!Directory.Exists(root))
                {
                    // Directory; System.IO'nun bir class'ıdır. Buradaki işlem tam olarak şu. Bu Upload metodumun parametresi olan string root CarManager'dan gelmekte
                    // CarImageManager içerisine girdiğinizde buraya parametre olarak *PathConstants.ImagesPath* böyle bir şey gönderilidğini görürsünüz. PathConstants clası içerisine girdiğinizde string bir ifadeyle bir dizin adresi var. O adres bizim yükleyeceğimiz dosyaların kayıt edileceği adres burada *Check if a directory Exists* ifadesi şunu belirtiyor dosyanın kaydedileceği adres dizini var mı? varsa if yapısının kod bloğundan ayrılır eğer yoksa içinde ki kodda dosyaların kayıt edilecek dizini oluşturur.
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);  // Seçmiş olduğumuz dosyanın uzantısını elde ediyoruz.
                string guid = Guid.NewGuid().ToString();  // Core.Utilities.Helpers.GuidHelper klasöründeki GuidManager class'ında oluşturduğumuz eşsiz ismi alıyoruz.
                string filePath = guid + extension;  // İsim ile uzantıyı birleştiriyoruz.

                using (FileStream fileStream = File.Create(root + filePath))  // FileStream class'ının bir örneği oluşturuldu, sonrasında ise "root + filePath" yolunda bir dosya oluşturur.
                {
                    file.CopyTo(fileStream);  // Yukarıdan gelen IFormFile türündeki file dosyasını nereye kopyalayacağını belirttik.
                    fileStream.Flush();  // Arabellekte silme.
                    return filePath;  // Sql servere dosya eklenirken adı ile eklenmesi için dosyamızın tam adını geri döndürüyoruz.
                }
            }
            return null;
        }
    }
}
