# MovieTicketCounter --> Controllers --> TicketController.cs - This file contains all the endpoints required for the Movie Ticket System. The api's are provided with explanations in form of comments.
MovieTicketCounter --> Web.Config - Add the connection string from TicketDataAccess project App.Config
TicketDataAccess is the class library that uses EntityFramework to generate model that echoes the [dbo].[MovieTicket] table in SQL server.

Table in database -

CREATE TABLE [dbo].[MovieTicket](
	[TicketId] [int] NOT NULL AUTO_INCREMENT,
	[UserName] [varchar](max) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[MovieName] [varchar](max) NOT NULL,
	[isExpired] [bit] NOT NULL,
	PRIMARY KEY (TicketId)
	)
  
  
 TicketController.cs EndPoints -
  
  http://localhost:64131/api/Ticket?userName=Saksham&phoneNumber=999999999&date=2020,09,24 -- POST
	http://localhost:64131/api/Ticket?ticketId=3&date=2019,09,01 -- PUT
	http://localhost:64131/api/Ticket?date=2019,09,01 -- GET
	http://localhost:64131/api/Ticket/4 -- DELETE
	http://localhost:64131/api/Ticket?ticketId=3 -- GET
	http://localhost:64131/api/Ticket -- PUT
	http://localhost:64131/api/Ticket -- DELETE
