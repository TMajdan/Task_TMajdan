﻿namespace Task_TMajdan.Src.DomainModels.Users
{
    using System.Collections.Generic;
    using Task_TMajdan.SeleniumFramework.Support.Enums;
    using Task_TMajdan.Src.Support;

    public static class UsersLists
    {
        public static List<UserData> Users = new List<UserData>()
        {
            new UserData
            {
                FirstName = "TOMASZ",
                LastName = "M",
                Salutations = "Mr",
                Email = "test@test.test",
                Phone = TestDataUtils.GenerateRandomNumber(999999999).ToString(),
                Website = "www.test.test",
                Role = Role.CEO,
                Category = new List<Category> { Category.Customers, Category.Suppliers },
            },

            new UserData
            {
                FirstName = "Anna",
                LastName = "Kowalska",
                Salutations = "Ms",
                Email = "anna.kowalska@test.test",
                Phone = TestDataUtils.GenerateRandomNumber(999999999).ToString(),
                Website = "www.annakowalska.test",
                Role = Role.Admin,
                Category = new List<Category> { Category.Customers, Category.Suppliers },
            }
        };
    }
}