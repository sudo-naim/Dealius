# Dealius Tests Automation Project
This document provides all the pre-requisites, setup data and tools needed for running the automated tests.  
<br>
  
## Pre-requisites for running the automation project
Create Office:
- Name: QA Office  

Create User 1 (Broker):
- Name: User Broker
- Role: Broker
- Office: QA Office
- Email address: user-broker1@dealius.com  

Create User 2 (Office Admin):
- Name: User Office Admin
- Role: Office Admin
- Office: QA Office
- Email address: user-officeadmin@dealius.com  

Add Deal Approval Workflow:
- Select QA Office
- Add Approver: User Office  

Franchise Information:
- Franchise fee: 2%  

Deal Default settings:
- Office settings
    - Office: QA Office
    - Lease Rate Type: Per Year  
<br>
<br>

## Tools

To run the automated tests one should install **Visual Studio**.  
After installing Visual Studio, one should install **Specflow** extension on Visual sudio  
*Steps to install Specflow:*
1. Open Visual Studio
2. Click **Extensions** on top menu bar of Visual Studio
3. Click **Manage Extensions** on the dropdown menu
4. Click **Online** on the left of the pop up displayed
5. Search for Specflow on far right search input field
6. Download **Specflow for Visual Studio 2019**  
<br>
<br>

## How to run the automated tests
**How to run a single scenario**:
1. Open Visual Studio
2. Click **View** on top menu bar of Visual Studio
3. Click **Text Explorel**
4. Build solution
5. On the **Test Explorel** select the scenario you want to run
6. Click run or Ctrl + R, T  

**How to run all tests**:
1. Open Visual Studio
2. Click **View** on top menu bar of Visual Studio
3. Click **Text Explorel**
4. Build solution
5. On the **Test Explorel** click run all button or Ctrl + R, V

**How to run all tests on powershell**:
1. Open Windows Power Shell
2. Navigate to the automation project folder
3. Use **command**: *dotnet test*
