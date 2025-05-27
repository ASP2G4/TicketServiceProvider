TicketServiceProvider

TicketServiceProvider is a C# service that receives ticket information via gRPC from the EventServiceProvider and stores it in a database. The service exposes a read-only API (via Swagger) to fetch tickets by event ID.
Features

    Receives ticket data through gRPC from EventServiceProvider

    Persists tickets in a database

    Provides a RESTful API to fetch tickets by event ID

    API documentation available through Swagger UI

    Read-only API (no ticket creation or deletion via REST)

Technologies

    C#

    gRPC for inter-service communication

    REST API with Swagger for ticket retrieval

    Database (specify which, e.g., SQL Server, PostgreSQL, etc.)

Getting Started
Prerequisites

    .NET SDK (version X.X)

    Database setup and connection string configured

    EventServiceProvider running and sending ticket data via gRPC

Installation

    Clone the repository:

git clone https://github.com/yourusername/TicketServiceProvider.git
cd TicketServiceProvider

Configure the database connection in appsettings.json.

Build and run the project:

    dotnet build
    dotnet run

Usage

    The service listens for gRPC messages from EventServiceProvider.

    To fetch tickets for a specific event, use the REST API:

GET /api/tickets?eventId={eventId}

Swagger UI is available at:

    http://localhost:{port}/swagger

API Reference
GET /api/tickets

Retrieve tickets based on the event ID.

Query Parameters:
Parameter	Type	Description
eventId	string	The ID of the event.

Response:

    200 OK with a list of tickets.

    404 Not Found if no tickets are found for the given event ID.
