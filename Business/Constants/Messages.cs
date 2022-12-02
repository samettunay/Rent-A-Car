using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araba eklendi";
        public static string RentalAdded = "Rental eklendi";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string CustomerAdded = "Müşteri eklendi";

        public static string CarUpdated = "Araba güncellendi";
        public static string RentalUpdated = "Rental güncellendi";
        public static string UserUpdated = "Kullanıcı güncellendi";
        public static string CustomerUpdated = "Müşteri güncellendi";

        public static string CarDeleted = "Araba silindi";
        public static string RentalDeleted = "Rental silindi";
        public static string UserDeleted = "Kullanıcı silindi";
        public static string CustomerDeleted = "Müşteri silindi";
        public static string MaintenanceTime = "Sistem Bakımda!";
        public static string DescriptionInvalid = "Açıklama en az 10 karakter içerebilir!";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
        public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string ProductNameAlreadyExists = "Araba ismi zaten mevcut";

        public static string ThisCarIsAlreadyRentedInSelectedDateRange = "Araba bu tarihler arasında zaten kiralanmıştır.";

        public static string RentalSuccessful = "Araç kiralandı.";
    }
}
