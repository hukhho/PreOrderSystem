# Pre Order Platform

This project provides a pre-order platform consisting of a web application and a Microsoft SQL Server database, both packaged as Docker containers for easy deployment and scalability.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

* [Docker ↗](https://www.docker.com/products/docker-desktop)
* [Docker Compose ↗](https://docs.docker.com/compose/install/)

## Usage

To use the Pre Order Platform, follow these steps:

1. Clone this repository to your local machine:

   `````bash
   git clone <repository-url>
   ```

   Replace `<repository-url>` with the URL of this repository.

2. Navigate to the directory containing the `docker-compose.yml` file:

   ````bash
   cd <repository-path>
   ```

   Replace `<repository-path>` with the path of the directory in your local machine.

3. Run the following command to start the services:

   ````bash
   docker-compose up --build
   ```

   This command will start the web application and the database as defined in the `docker-compose.yml` file.

The web application can be accessed at `http://localhost:5019`. The database server can be accessed at `localhost,1433` with login `SA` and password `YourStrong!Passw0rd`. 

You may replace `YourStrong!Passw0rd` with your desired password in the `docker-compose.yml` file. Please ensure that the same password is used in both the `db` and `web` services.

The `db` service uses a volume named `db_data` for persisting database data across container restarts.

Both services are connected to a Docker network named `app-network`, which allows them to communicate with each other.

## Note

http://localhost:5019/swagger/

## Contact

If you want to contact me, you can reach me at `<your-email>`.

## License
