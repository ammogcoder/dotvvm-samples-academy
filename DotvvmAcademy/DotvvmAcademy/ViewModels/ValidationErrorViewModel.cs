﻿using DotvvmAcademy.BL.DTO;

namespace DotvvmAcademy.ViewModels
{
    public class ValidationErrorViewModel
    {
        public int EndPosition { get; set; }

        public bool IsGlobal { get; set; }

        public string Message { get; set; }

        public int StartPosition { get; set; }

        public static ValidationErrorViewModel Create(ValidationErrorDTO error)
        {
            var viewModel = new ValidationErrorViewModel
            {
                EndPosition = error.EndPosition,
                StartPosition = error.StartPosition,
                Message = error.Message,
                IsGlobal = error.IsGlobal
            };
            return viewModel;
        }
    }
}