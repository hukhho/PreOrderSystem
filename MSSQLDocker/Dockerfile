FROM mcr.microsoft.com/mssql/server:2019-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd
ENV MSSQL_PID=Developer

WORKDIR /var/opt/mssql

# Copy SQL scripts
COPY ./init.sql .
COPY ./entrypoint.sh .

# RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
#     && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "YourStrong!Passw0rd" -d master -i init.sql

EXPOSE 1433

