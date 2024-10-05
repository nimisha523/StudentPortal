﻿namespace StudentPortal.Web.Models.ViewModels
{
    public class AddStudentViewModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public bool Subscribed { get; set; }

        public DateOnly Dob { get; set; }
    }
}
