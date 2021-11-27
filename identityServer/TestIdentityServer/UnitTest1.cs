using IdentityServer4.Models;
using NUnit.Framework;

namespace TestIdentityServer
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var secreto = new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256());

            Assert.AreEqual("fU7fRb+g6YdlniuSqviOLWNkda1M/MuPtH6zNI9inF8=", secreto.Value);


            var sercretoApi = new Secret("amarillo".Sha256());
            Assert.AreEqual("4HGYugUKMklfReduECoo6bBigov3pR/Y7URi9T4CBdk=", sercretoApi.Value);
        }
    }
}