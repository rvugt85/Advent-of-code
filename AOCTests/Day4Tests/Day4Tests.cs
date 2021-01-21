using AOC_2020;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AOCTests
{
    [TestClass]
    public class Day4Tests
    {
        static string Part1Path = "\\AOCTests\\Day4Tests\\Day4TestInput.txt";  

        private Day4 Day4 = new Day4();
        private MainProgram mainProgram = new MainProgram();

        [TestMethod]
        [DataRow(0, 1937, 2017, 2020, HeightKey.cm, 183, "#fffffd", EyeColor.gry, "860033327", 147)]
        [DataRow(1, 1929, 2013, 2023, HeightKey.Unknown, null, "#cfa07d", EyeColor.amb, "028048884", 350)]
        [DataRow(2, 1931, 2013, 2024, HeightKey.cm, 179, "#ae17e1", EyeColor.brn, "760753108", null)]
        [DataRow(3, null, 2011, 2025, HeightKey.@in, 59, "#cfa07d", EyeColor.brn, "166559648", null)]
        public void DataProcessor(int paragraphindex, int? birthyear, int? issueYear, int? expirationYear, 
            HeightKey heightkey, int? heightvalue, string hairColor, EyeColor eyeColor, string passportId, int? countryId)
        {
            var paragraphs = CreateParagraphs(Part1Path);

            var passportData = Day4.CreatePassportData(paragraphs[paragraphindex]);

            Assert.AreEqual(birthyear, passportData.BirthYear);
            Assert.AreEqual(issueYear, passportData.IssueYear);
            Assert.AreEqual(expirationYear, passportData.ExpirationYear);
            Assert.AreEqual(heightkey, passportData.HeightData?.HeightKey);
            Assert.AreEqual(heightvalue, passportData.HeightData?.HeightValue);
            Assert.AreEqual(hairColor, passportData.HairColor);
            Assert.AreEqual(eyeColor, passportData.EyeColor);
            Assert.AreEqual(countryId, passportData.CountryId);
        }

        [TestMethod]
        [DataRow(1937, 2017, 2020, HeightKey.cm, 183, "#fffffd", EyeColor.gry, "860033327", 147, true)]
        [DataRow(1929, 2013, 2023, HeightKey.Unknown, null, "#cfa07d", EyeColor.amb, "028048884", 350, false)]
        [DataRow(1931, 2013, 2024, HeightKey.cm, 179, "#ae17e1", EyeColor.brn, "760753108", null, true)]
        [DataRow(null, 2011, 2025, HeightKey.@in, 59, "#cfa07d", EyeColor.brn, "166559648", null, false)]
        public void ValidityChecker(int? birthyear, int? issueYear, int? expirationYear,
            HeightKey heightKey, int heightValue, string hairColor, EyeColor eyeColor, string passportId, int? countryId, bool expectedValidity)
        {
            var passportData = new PassPortData
            {
                BirthYear = birthyear,
                IssueYear = issueYear,
                ExpirationYear = expirationYear,
                HeightData = new HeightData { HeightKey = heightKey, HeightValue = heightValue },
                HairColor = hairColor,
                EyeColor = eyeColor,
                PassportId = passportId,
                CountryId = countryId
            };

            var actualValidity = Day4.PassportIsValid(passportData);

            Assert.AreEqual(expectedValidity, actualValidity);
        }

        [TestMethod]
        public void Part1FullExampleChecker()
        {
            var paragraphs = CreateParagraphs(Part1Path);

            var result = Day4.CalculateValidPassports(paragraphs);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CheckValidPassports()
        {
            var paragraphs = CreateParagraphs("\\AOCTests\\Day4Tests\\ValidPassports.txt");

            Assert.AreEqual(paragraphs.Count, Day4.CalculateValidPassports(paragraphs));
        }

        [TestMethod]
        public void CheckInvalidPassports()
        {
            var paragraphs = CreateParagraphs("\\AOCTests\\Day4Tests\\InvalidPassports.txt");

            Assert.AreEqual(0, Day4.CalculateValidPassports(paragraphs));
        }

        private List<string> CreateParagraphs(string path)
        {
            return mainProgram.ConvertFileToParagraphs(path);
        }
    }
}
