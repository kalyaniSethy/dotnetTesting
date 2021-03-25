FROM mcr.microsoft.com/mssql/server:latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=some_n0ice_w0rt

COPY ./data /
ENTRYPOINT [ "/bin/bash", "entrypoint.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]
