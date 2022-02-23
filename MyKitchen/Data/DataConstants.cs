namespace MyKitchen.Data
{
    public class DataConstants
    {
        public class Kitchen
        {
            public const int DescriptionMinLength = 20;
        }
        public class Manufacturer
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 300;
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 30;
        }

        public class Category
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 70;
        }

        public class Color
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
        }
        public class Country
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }
        public class City
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }
    }
}


