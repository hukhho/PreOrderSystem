# Use a lightweight base image with sqlcmd installed
FROM mcr.microsoft.com/mssql-tools

WORKDIR /var/opt/mssql

# Copy SQL scripts
COPY ./init.sql .

# Wait for the SQL Server to start and run the script
CMD /bin/bash -c 'while ! /opt/mssql-tools/bin/sqlcmd -S db -U SA -P "YourStrong!Passw0rd" -Q "SELECT 1" | tee -a startup.log; do sleep 1; done; /opt/mssql-tools/bin/sqlcmd -S db -U SA -P "YourStrong!Passw0rd" -d master -i init.sql' | tee -a startup.log