> # Lab_Master
# Lab_Master

**Lab_Master** is a desktop application designed to streamline the management of IT Department labs. It provides detailed insights into labs, computers, software, and students while offering group and student distribution automation.

---

## Features and Functionalities

### Lab Management
1. **Lab Listings:**
   - View a comprehensive list of all labs in the IT Department.
   - Visualize each lab with a detailed **sketch**.

2. **Lab Efficiency Analysis:**
   - Assess lab statuses: **exists**, **outside**, **working well**, **has a problem**, and calculate efficiency percentage.
   - Generate efficiency reports for individual labs or all labs.

---

### Computer Management
1. **Computer Details:**
   - List all computers in a lab with the following attributes:
     - **Hardware Details:** `caseSerialNumber`, `screenSerialNumber`, `computerName`, `processor`, `RAM`, `storage`, `graphicsCard`, `operatingSystem`.
     - **Network Details:** `MACAddress`, `connectionToNetworkStatus`.
     - **Operational Status:** `[status]`.

2. **Installed Software:**
   - Display a list of installed software for each computer:
     - **Software Name**, **Version**, **Category**.

3. **Assigned Students:**
   - List students using a computer with:
     - **Student Name**, **Group**, **Number**.

---

### Report Management
- Generate detailed reports on labs, computers, and software.
- Save reports in **TXT** or **XLSX** formats or print them directly.

---

## Dashboard Capabilities
1. **CRUD Operations (Create, Read, Update, Delete):**
   - Manage labs, computers, groups, students, and software with ease.

2. **Automation:**
   - Distribute groups among labs automatically.
   - Assign students within groups to specific computers manually or automatically.

---

## Primary Functionality
The app automates lab and computer assignments:
1. **Group Distribution:**
   - Assign groups to labs intelligently based on capacity or criteria.

2. **Student Assignment:**
   - Assign students in each group to computers within their respective labs.

---

## How to Use
1. Use the dashboard to manage labs, computers, groups, students, and software.
2. Analyze lab efficiency and generate reports.
3. Automatically distribute groups and students across labs and computers.

---

## Output
- View lab and computer details in-app.
- Generate and save reports in **TXT** or **XLSX** formats.
- Print reports directly for quick access.
  
---

## Used Technologies 
- C#
- SQL Server
- WPF
- ADO.NET

