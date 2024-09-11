# Appium Mobile Automation Tests

[![C#](https://img.shields.io/badge/Made%20with-C%23-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-5C2D91.svg)](https://dotnet.microsoft.com/)
[![Android Studio](https://img.shields.io/badge/Built%20with-Android%20Studio-3DDC84.svg)](https://developer.android.com/studio)
[![Appium](https://img.shields.io/badge/tested%20with-Appium-41BDF5.svg)](https://appium.io/)

### This is a test project for Front-End Test Automation July 2024 Course @ SoftUni

## Overview
This repository demonstrates how to automate testing of Android mobile applications using **Appium** and **Page Object Model (POM)**.

## 1. "Summator" Page Object Model (POM)
### 1.1 Automated Testing Scenario

The testing scenario involves the following steps:

1. **Open the Summator App.**
2. **Perform tests with both valid and invalid inputs:**
   - For valid inputs, verify the calculation results are correct.
   - For invalid inputs, ensure that an "error" message is displayed in the result field.

### 1.3 Configure Appium Inspector

To run tests, follow these steps to configure Appium Inspector:

1. **Start the Appium Server.**  
   If you're using the web version, start it with `--allow-cors` enabled.
   
2. **Start your Android Virtual Device (AVD).**
   
3. **Install the application** on your AVD by dragging and dropping the `.apk` file into the emulator.

4. **Set up Appium Inspector:**
   - Provide the host and port of your Appium server (e.g., `http://127.0.0.1:4723`).
   - Add the desired capabilities:
     - `automationName`: `UiAutomator2`
     - `platformName`: Android (use `appium driver list` to get the value)
     - `platformVersion`: Get the Android version using `adb shell getprop ro.build.version.release`.
     - `deviceName`: Use `adb devices` to find the connected device name.
     - `app`: Path to the `.apk` file on your computer.
