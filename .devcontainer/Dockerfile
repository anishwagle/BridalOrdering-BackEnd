FROM mcr.microsoft.com/dotnet/sdk:5.0

RUN useradd -m -s $(which bash) dev
RUN mkdir /app && chown dev:dev /app
USER dev
