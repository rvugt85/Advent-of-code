using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AOC_2020
{
    public class Day4
    {
        static string path = "\\AOC 2020\\Day4\\InputDay4.txt";
        readonly string HaircolorPattern = @"^#[0-9,a-f]{6}$";
        readonly string PassportIdPattern = @"^[0-9]{9}$";

        private readonly MainProgram mainProgram = new MainProgram();

        public void RunDay4()
        {
            var paragraphs = mainProgram.ConvertFileToParagraphs(path);

            var answer = CalculateValidPassports(paragraphs);

            Console.WriteLine($"The count of valid passports is {answer}");
        }

        public int CalculateValidPassports(List<string> paragraphs)
        {
            int validPassports = 0;

            foreach(var paragraph in paragraphs)
            {
                var passportData = CreatePassportData(paragraph);

                if (PassportIsValid(passportData))
                {
                    validPassports++;
                }    
            }

            return validPassports;
        }

        public PassPortData CreatePassportData(string paragraph)
        {
            var passportData = new PassPortData { HeightData = new HeightData() };

            var keyAndValues = mainProgram.SplitParagraphsByLineEndingsAndSpaces(paragraph);

            foreach (var keyAndValue in keyAndValues)
            {
                var split = keyAndValue.Split(':');
                var key = split[0];
                var value = split[1];
                switch (key)
                {
                    case "byr":
                        passportData.BirthYear = int.Parse(value);
                        break;
                    case "iyr":
                        passportData.IssueYear = int.Parse(value);
                        break;
                    case "eyr":
                        passportData.ExpirationYear = int.Parse(value);
                        break;
                    case "hgt":
                        passportData.HeightData = CreateHeightData(value);
                        break;
                    case "hcl":
                        passportData.HairColor = value;
                        break;
                    case "ecl":
                        if(Enum.TryParse(value, out EyeColor eyeColor))
                        {
                            passportData.EyeColor = eyeColor;
                        }
                        break;
                    case "pid":
                        passportData.PassportId = value;
                        break;
                    case "cid":
                        passportData.CountryId = int.Parse(value);
                        break;
                }
            }

            return passportData;
        }

        public bool PassportIsValid(PassPortData passportData)
        {
            if (passportData.BirthYear == null
                || passportData.IssueYear == null
                || passportData.ExpirationYear == null
                || passportData.HeightData == null
                || passportData.HairColor == null
                || passportData.EyeColor == null
                || passportData.PassportId == null)
                return false;

            if (!Regex.Match(passportData.PassportId, PassportIdPattern).Success)
                return false;
            if (passportData.BirthYear < 1920 || passportData.BirthYear > 2002)
                return false;
            if (passportData.IssueYear < 2010 || passportData.IssueYear > 2020)
                return false;
            if (passportData.ExpirationYear < 2020 || passportData.ExpirationYear > 2030)
                return false;
            if (!IsHeightValid(passportData.HeightData))
                return false;
            if (!Regex.Match(passportData.HairColor, HaircolorPattern).Success)
                return false;

            return true;
        }

        private HeightData CreateHeightData(string keyAndValue)
        {
            var heightData = new HeightData();

            var keyInString = keyAndValue.Substring(keyAndValue.Length - 2, 2);

            heightData.HeightKey = Enum.Parse<HeightKey>(keyInString);
            if (heightData.HeightKey == HeightKey.cm || heightData.HeightKey == HeightKey.@in)
            {
                if (int.TryParse(keyAndValue.Substring(0, keyAndValue.Length - 2), out var value))
                {
                    heightData.HeightValue = value;
                }                
            }
            return heightData;
        }

        private bool IsHeightValid(HeightData heightData)
        {
            var heightValue = heightData.HeightValue;
            var heightKey = heightData.HeightKey;

            if(heightKey == HeightKey.cm)
            {
                return heightValue >= 150 && heightValue <= 193;
            }
            if(heightKey == HeightKey.@in)
            {
                return heightValue >= 59 && heightValue <= 76;
            }
            else { return false; }
        }

        private bool IsHairColorValid(string hairColor)
        {
            throw new NotImplementedException();
        }
    }

    public class PassPortData
    {
        public int? BirthYear { get; set; }
        public int? IssueYear { get; set; }
        public int? ExpirationYear { get; set; }
        public HeightData HeightData {get;set;}
        public string HairColor { get; set; }
        public EyeColor? EyeColor { get; set; }
        public string PassportId { get; set; }
        public int? CountryId { get; set; }
    }

    public class HeightData
    {
        public HeightKey HeightKey { get; set; }
        public int? HeightValue { get; set; }
    }

    public enum HeightKey
    {
        Unknown,
        cm,
        @in
    }

    public enum EyeColor
    {
        amb,
        blu,
        brn,
        gry,
        grn, 
        hzl,
        oth
    }
}
