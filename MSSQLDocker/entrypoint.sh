#!/bin/bash
set -e

# Start the SQL Server
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
sleep 30s

echo "Running initialization script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "YourStrong!Passw0rd" -d master -i init.sql

echo "Initialization script completed"
tail -f /dev/null