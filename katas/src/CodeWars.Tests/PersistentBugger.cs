namespace CodeWars.Tests
{
    

    public class PersistentBugger
    {
        [Test]
        public void Test1() {
            Console.WriteLine("****** Basic Tests");    
            Assert.That(Persist.Persistence(39), Is.EqualTo(3));
            Assert.That(Persist.Persistence(4), Is.EqualTo(0));
            Assert.That(Persist.Persistence(25), Is.EqualTo(2));
            Assert.That(Persist.Persistence(999), Is.EqualTo(4));
        }

        
    }
    public class Persist {
        public static int Persistence(long n) 
        {
            int times = 0;
            
            while (n > 9)
            {
                
                var numbers = n.ToString().Select(c => int.Parse(c.ToString()) );                
                
                var x = 1;
                foreach(var d in numbers) {
                    x *= d;    
                }
                n = x;
                
                times++;
                
            }
      
            return times;
        }
    }
}