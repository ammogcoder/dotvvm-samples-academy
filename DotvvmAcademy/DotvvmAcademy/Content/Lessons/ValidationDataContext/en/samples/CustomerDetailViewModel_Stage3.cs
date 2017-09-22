﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DotvvmAcademy.Tutorial.ViewModels
{
    public class Lesson4ViewModel
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string ZIP { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        // добавьте свойство SubscriptionFrom
        // добавьте свойство SubscriptionTo
    }
}