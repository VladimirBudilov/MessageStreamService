# MessageStreamService

This project is a simple messaging service implemented as a test task. The service consists of three components: a web server, a PostgreSQL database, and three client applications.

## Project Components

1. **Web Server**
    - Built with .NET 9.0.
    - REST API with SignalR library for WebSocket communication.
    - Swagger documentation available at `http://localhost:5000/swagger`.

2. **Frontend**
    - Built with Angular 18.
    - Runs on `http://localhost:4200`.

3. **Database**
    - PostgreSQL v17 database.

4. **Docker**
    - All components are containerized.
    - Docker Compose file named `compose.yml` is stored in the root folder.

## Features

- **Client Applications**
    - **Client 1**: Sends messages (up to 128 characters).
    - **Client 2**: Displays messages in real time via WebSocket.
    - **Client 3**: Fetches and displays a list of messages from the last 10 minutes.

- **API Endpoints**
    - `POST /api/messages`: Send a message.
    - `GET /api/messages?from={-10mins:datetime}&to={now:datetime}`: Retrieve messages within a date range.

- **Database and Logging**
    - SQL database (PostgreSQL) with a DAL layer (no ORM).
    - Logging with Serilog.
  
## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/VladimirBudilov/MessageStreamService.git
2. Ensure Docker is installed and running.
3. Start the services using the command:
   ```bash
   docker-compose up --build -d
