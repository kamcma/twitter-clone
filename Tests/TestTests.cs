using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterClone.Data.Models;

namespace TwitterClone.Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void HashAndVerifyPassword()
        {
            string password = "p@ssw0rd";
            User user = new User()
            {
                FirstName = "Joe",
                LastName = "Smith",
                EmailAddress = "joe.smith@mailhost.com"
            };
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, password);

            Assert.AreNotEqual(password, user.PasswordHash);
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            Assert.AreEqual(result, PasswordVerificationResult.Success);
        }


    }
}