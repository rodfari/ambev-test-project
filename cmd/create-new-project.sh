#!/bin/bash
set -e  # Stop execution if any command fails
read -p "Enter the project name: " PROJECT_NAME
if [ -z "$PROJECT_NAME" ]; then
    echo "Project name is required"
    exit 1
fi
#PROJECT_NAME=CleanArchitecture
BACKEND=Backend
API=Backend/Consumers/Api
DOMAIN=Backend/Core/Domain
APPLICATION=Backend/Core/Application
INFRASTRUCTURE_PGSQL=Backend/Infrastructure/Persistence/pgSQL
TESTS=Backend/Tests
# Step 1: Create the directory structure
#
# CREATE FOLDER STRUCTURE
#
######
mkdir -p $API && $DOMAIN && mkdir -p $APPLICATION && mkdir -p $INFRASTRUCTURE_PGSQL  && mkdir -p $TESTS
echo 'Directory structure created'


#
# DOMAIN CLASS LIBRARY
#
######
dotnet new classlib -o $DOMAIN
mkdir   $DOMAIN/Entities $DOMAIN/Enums $DOMAIN/Exceptions $DOMAIN/Contracts $DOMAIN/StatusCodes
#copy the files from the template to the project
cp cmd/templates/IGenericRepository.cs $DOMAIN/Contracts/IGenericRepository.cs
cp cmd/templates/DefaultEntity.cs $DOMAIN/Entities/DefaultEntity.cs
rm $DOMAIN/Class1.cs
echo 'Domain project created'


#
# APPLICAITON CLASS LIBRARY 
#
######
dotnet new classlib -o $APPLICATION
dotnet add $APPLICATION reference $DOMAIN

mkdir $APPLICATION/Features 

cd $APPLICATION
dotnet add package FluentValidation
dotnet add package FluentValidation.DependencyInjectionExtensions
dotnet add package MediatR 
dotnet add package AutoMapper 
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package BCrypt.Net-Next
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.11
cd -  # Return to the previous directory
rm $APPLICATION/Class1.cs
echo '\n\r\n\r\n\rPackage references added to Application project\n\r\n\r\n\r'

#
# APPLICATION TEST 
#
######
TESTS_APPLICATION=$TESTS/Application
dotnet new xunit -n TestsApplication -o $TESTS_APPLICATION
rm $TESTS_APPLICATION/UnitTest1.cs
cd $TESTS_APPLICATION
dotnet add package Moq
dotnet add package Moq.AutoMock
dotnet add package AutoFixture
dotnet add package Shouldly
cd -  # Return to the previous directory
dotnet add $TESTS_APPLICATION reference $APPLICATION

#
# DOMAIN TEST 
#
######
TESTS_DOMAIN=$TESTS/Domain
dotnet new xunit -n TestsDomain -o $TESTS_DOMAIN
rm $TESTS_DOMAIN/UnitTest1.cs
cd $TESTS_DOMAIN
dotnet add package Moq
dotnet add package Moq.AutoMock
dotnet add package AutoFixture
dotnet add package Shouldly
cd -  # Return to the previous directory
dotnet add $TESTS/Domain reference $DOMAIN

#
# INFRASRUCTURE CLASS LIBRARY
#
######
# vars
REPOSITORY=$INFRASTRUCTURE_PGSQL/Repository
CONFIGURATIONS=$INFRASTRUCTURE_PGSQL/Configurations

dotnet new classlib -o $INFRASTRUCTURE_PGSQL
dotnet add $INFRASTRUCTURE_PGSQL reference $DOMAIN
dotnet add $INFRASTRUCTURE_PGSQL reference $APPLICATION

mkdir $CONFIGURATIONS $REPOSITORY $INFRASTRUCTURE_PGSQL/Seeders

cd $INFRASTRUCTURE_PGSQL
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.11
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.11
cd -  # Return to the previous directory
rm $INFRASTRUCTURE_PGSQL/Class1.cs
#copy the files from the template to the project
cp cmd/templates/GenericRepository.cs $REPOSITORY/GenericRepository.cs
cp cmd/templates/DataContext.cs $INFRASTRUCTURE_PGSQL/DataContext.cs
cp cmd/templates/PostgresRegistration.cs $INFRASTRUCTURE_PGSQL/PostgresRegistration.cs

echo '\n\r\n\r\n\rPackage references added to Infrastructure project\n\r\n\r\n\r'

#
# 
#
######

dotnet new webapi -o $API --use-controllers
dotnet add $API reference $DOMAIN
dotnet add $API reference $APPLICATION
dotnet add $API reference $INFRASTRUCTURE_PGSQL
echo 'Web API project created'

#
# API TEST
# Controller tests
######

TESTS_API=$TESTS/Api

dotnet new xunit -n TestsApi -o $TESTS_API
dotnet add $TESTS_API reference $API
dotnet add $TESTS_API reference $APPLICATION

rm $TESTS_API/UnitTest1.cs
cd $TESTS_API
dotnet add package Moq
dotnet add package Moq.AutoMock
dotnet add package AutoFixture
dotnet add package Shouldly
cd -  # Return to the previous directory
echo 'API test project created'

#
# 
#
######
dotnet new sln -n $PROJECT_NAME -o $BACKEND
# cd $BACKEND
dotnet sln $BACKEND add $API
dotnet sln $BACKEND add $DOMAIN
dotnet sln $BACKEND add $APPLICATION
dotnet sln $BACKEND add $INFRASTRUCTURE_PGSQL
dotnet sln $BACKEND add $TESTS_APPLICATION
dotnet sln $BACKEND add $TESTS_DOMAIN
dotnet sln $BACKEND add $TESTS_API
# echo 'Solution file created'

dotnet build $BACKEND

echo 'Project created successfully'
