# Events
This is a basic website for managing events.

![Screenshot 2024-07-10 101153](https://github.com/Mahalakshmi-kalidass/EventsManagement/assets/89296562/0c726f34-e6d6-4e9f-b885-7890d2765dd0)


![image](https://github.com/user-attachments/assets/b8b28597-bebe-49a0-b1fc-c2ca235da5e1)



**Features**

**Create new events**: Users can add new events with a title, description, date and time.

**View existing events**: A list of existing events is displayed with their details.

**Edit events**: Users can modify the details of existing events.

**Delete events**: Users can remove events.

**Error logging**: Errors are logged in a table in the database for debugging and troubleshooting purposes.

**Technologies**

  ASP.Net Core MVC
  
  ASP.NET Core Web API, Swagger
  
  C#
  
  HTML
  
  CSS, Bootstrap
  
  Jquery
  
  Entity Framework Core (for database interactions)

  
**Project Structure**

Controllers: HomeController.cs handles HTTP requests and interacts with the model.

Models: model represents the event entity.

Views: Event views (e.g. Index.cshtml, Create.cshtml, Edit.cshtml) display the user interface.

API Controllers: EventController.cs handles API requests and interacts with the model.

Database: The application uses a database to store event data.

**Future Improvements**

Filtering and sorting: Implement filters and sorting options for the event list.

User authentication: Add user login and registration to allow for user-specific events.

Calendar integration: Integrate a calendar view to visualize the events.
