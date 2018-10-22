﻿#load "./00_constants.csx"

Unit.SetFileName("ProfileDetailViewModel.cs");
Unit.SetDefault("./ProfileDetailViewModel_10.cs");
Unit.SetCorrect("./ProfileDetailViewModel_20.cs");

Unit.GetType<string>()
    .Allow();

Unit.GetType("System.Void")
    .Allow();

var profileType = Unit.GetType(Profile);
profileType.Allow();
profileType.GetProperty("FirstName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
profileType.GetProperty("LastName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter()
    .Allow();
profileType.GetProperty("Country")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
profileType.GetProperty("City")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();
profileType.GetProperty("Street")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter();

var viewModelType = Unit.GetType(ProfileDetailViewModel);
viewModelType.GetProperty("Profile")
    .IsOfType(Profile)
    .HasGetter()
    .HasSetter()
    .Allow();
viewModelType.GetProperty("NewLastName")
    .IsOfType<string>()
    .HasGetter()
    .HasSetter()
    .Allow();