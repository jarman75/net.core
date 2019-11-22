using Domain.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.tests
{
    public class ValueObjectTests
    {
        

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_shortName()
        {

            var ex = Assert.Throws<ShortNameValidationException>(() => new ShortName(""));
            Assert.That(ex.Message == "The 'Name' is required.", ex.Message);

            ex = Assert.Throws<ShortNameValidationException>(() => new ShortName(new String('X', 16)));
            Assert.That(ex.Message == "The 'Name' supports a maximum of 15 characters.", ex.Message);


            //correct names comparer
            var name1 = new ShortName("Manuel");
            var name2 = new ShortName("Manuel");

            Assert.IsTrue(name1.ToString() == "Manuel");
            Assert.AreEqual(name1, name2);
        }

        [Test]
        public void Test_rangeDate_compararer()  {
            var range = new DateRange(new DateTime(2019,1,1), new DateTime(2019,1,2));
            var range2 = new DateRange(new DateTime(2019,1,1), new DateTime(2019,1,2));
            var range3 = new DateRange(new DateTime(2019,1,1), new DateTime(2019,1,3));

            Assert.AreEqual(range,range2);
            Assert.AreNotEqual(range,range3);
        }
        [Test]
        public void Test_rangeDate_endDate_whenNull_setTo_maxValue() {
            var range = new DateRange(new DateTime(2019,1,1), null);
            var range2 = new DateRange(new DateTime(2019,1,1), DateTime.MaxValue);
            Assert.AreEqual(range,range2);
        }
        [Test]
        public void Test_rangeDate_startDate_isLessOrEqualThan_endDate() {
            
            var ex = Assert.Throws<DateRangeValidationException>(() => new DateRange(new DateTime(2019,1,2), new DateTime(2019,1,1)));    
            Assert.That(ex.Message == "The 'startDate' must be less or equal than the 'endDate'.", ex.Message);

        }        
        [TestCase("01/01/2019", "01/03/2019", "01/12/2018", "31/12/2018", false)]        
        [TestCase("01/01/2019", "01/02/2019", "01/12/2018", "01/03/2019", true)]        
        [TestCase("01/01/2019", "01/02/2019", "01/12/2018", "01/02/2019", true)]        
        [TestCase("01/01/2019", "01/03/2019", "01/02/2019", "15/02/2019", true)]              
        [TestCase("01/01/2019", "01/03/2019", "01/03/2019", "31/12/2020", true)]        
        [TestCase("01/01/2019", "01/03/2019", "01/01/2019", "31/12/2020", true)]                
        
        public void Test_rangeDate_Overlap_inOther(string range1StartDate, string range1EndDate,string range2StartDate, string range2EndDate, bool result) {
            var dater1s = DateTime.Parse(range1StartDate);
            var dater1e = DateTime.Parse(range1EndDate);
            var dater2s = DateTime.Parse(range2StartDate);
            var dater2e = DateTime.Parse(range2EndDate);            
            var range1 = new DateRange(dater1s, dater1e);
            var range2 = new DateRange(dater2s, dater2e);
                        
            Assert.AreEqual(range1.Overlap(range2), result);                                    
        }
        
        [Test]
        public void Test_rageDate_overlap_inList() {
            var rangeList = new List<DateRange>() {
                new DateRange(new DateTime(2019,1,1), new DateTime(2019,1,31) ),
                new DateRange(new DateTime(2019,2,1), new DateTime(2019,2,28) ),
                new DateRange(new DateTime(2019,3,1), new DateTime(2019,3,21) ),
            };

            var newRange = new DateRange(new DateTime(2019,3,15), new DateTime(2019,3,21) );

            var result = rangeList.Any( x => x.Overlap(newRange) );
            Assert.IsTrue(result);

            newRange = new DateRange(new DateTime(2018,3,15), new DateTime(2018,3,21) );
            result = rangeList.Any( x => x.Overlap(newRange) );
            Assert.IsFalse(result);

        }

    }    

   
    
}