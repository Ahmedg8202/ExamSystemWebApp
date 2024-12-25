# **Exam System**

An exam management system designed to streamline the process of exam creation, administration, and evaluation. Built with **ASP.NET 8** and **Angular 18**, this application features a modern, scalable architecture and real-time notifications using **SignalR**.


## **Table of Contents**

1.  Features
    
2.  Technologies Used
    
3.  Setup Instructions
    
4.  Usage
    
5.  Architecture Overview
    
6.  Future Enhancements
    
7.  Contributors
    


## **Features**

### **For Students**:

-   Request exams.
    
-   Take multiple-choice exams.
    
-   View results and result history.
    

### **For Admins**:

-   Manage students and exams.
    
-   Manage subjects and its question library.

-   Define exam details and structure.
    
-   Receive real-time notifications for student actions.
    

### **Additional Features**:

-   Real-time updates via SignalR.
    
-   Role-based authentication and authorization.
    
-   Comprehensive exam history management.
    


## **Technologies Used**

### **Backend**:

-   ASP.NET Core
    
-   Entity Framework Core
    
-   SignalR for real-time updates
    
-   FluentValidation for input validation
    
-   JWT for authentication
    

### **Frontend**:

-   Angular 18 with standalone components
    
-   TypeScript
    
-   Bootstrap
    

### **Database**:

-   Microsoft SQL Server
    

### **Other Tools**:
-   Identity for user management

-   AutoMapper for object mapping
    
<!--
## **Setup Instructions**

### **Prerequisites**:

1.  [.NET SDK](https://dotnet.microsoft.com/download)
    
2.  [Node.js and npm](https://nodejs.org/)
    
3.  [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
    

### **Backend Setup**:

1.  Clone the repository:
    
    ```
    git clone https://github.com/your-repo/exam-system.git
    cd exam-system
    ```
    
2.  Configure the connection string in `appsettings.json`:
    
    ```
    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=ExamSystemDB;Trusted_Connection=True;"
    }
    ```
    
3.  Apply database migrations:
    
    ```
    dotnet ef database update
    ```
    
4.  Run the backend:
    
    ```
    dotnet run
    ```
    

### **Frontend Setup**:

1.  Navigate to the Angular project directory:
    
    ```
    cd exam-system-client
    ```
    
2.  Install dependencies:
    
    ```
    npm install
    ```
    
3.  Run the frontend:
    
    ```
    ng serve
    ```
    
4.  Open your browser at [http://localhost:4200](http://localhost:4200).
    

----------
-->
## **Usage**

1.  **Student Workflow**:
    
    -   Register and log in as a student.
        
    -   Request an exam or select an available one.
        
    -   Complete the exam within the allotted time.
        
    -   View results and result history.
        
2.  **Admin Workflow**:
    
    -   Log in with an admin account.
        
    -   Manage student accounts and exam definitions.
        
    -   Monitor exam requests and updates in real time.
        


## **Architecture Overview**

### **Backend**:

-   **Clean Architecture**: Separation of concerns between Core, Infrastructure, Application, and API layers.
    
-   **SignalR**: Real-time communication for notifications.
    

### **Frontend**:

-   **Modular Design**: Angular components and services are highly reusable.
    
-   **Standalone Components**: Leveraging Angular 17+ features for simplicity.
    

### **Database**:

-   **Entity Framework Core**: ORM for database interactions.
    
-   **SQL Server**: Relational database for data persistence.
    

<!--
## **Future Enhancements**

-   Add support for essay-based questions.
    
-   Implement role-specific dashboards.
    
-   Integrate AI-based cheating detection.
    
-   Add email notifications for exam results.
    

----------
-->
## **Contributors**

-   **Ahmed Gamal**  
    _Full-Stack Developer_  
    Connect on [LinkedIn](https://www.linkedin.com/in/ahmedg8202/) | [GitHub](https://github.com/Ahmedg8202/)
