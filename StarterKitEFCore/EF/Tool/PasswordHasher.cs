using System;
using System.Security.Cryptography;
using BCrypt.Net;

namespace StarterKit.EF.Tool
{
    public static class PasswordHasher
    {
        // Генерация хеша
        public static string HashPassword(this string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        // Проверка пароля
        public static bool VerifyPassword(this string hashedPassword, string password)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // Логируем ошибку или возвращаем false
                return false;
            }
        }
    }
}
