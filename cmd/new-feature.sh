#!/bin/bash
set -e  # Stop execution if any command fails
read -p "Enter the feature name: " FEATURE_NAME

if [ -z "$FEATURE_NAME" ]; then
    echo "Project name is required"
    exit 1
fi



BACKEND=Backend
API=Backend/Consumers/Api
DOMAIN=Backend/Core/Domain
APPLICATION=Backend/Core/Application
INFRASTRUCTURE_PGSQL=Backend/Infrastructure/Persistence/pgSQL
TESTS=Backend/Tests

FEATURE_FOLDER=$APPLICATION/Features/$FEATURE_NAME
mkdir -p $FEATURE_FOLDER

read -p "It is a command or a query? (c/q): " COMMAND_OR_QUERY

if [ -z "$COMMAND_OR_QUERY" ]; then
    echo "Command or Query is required"
    exit 1
fi

if [ "$COMMAND_OR_QUERY" = "c" ]; then
    cp cmd/templates/Application/commands/Command.sh $FEATURE_FOLDER/$FEATURE_NAME.cs
    sed -i '' "s/FEATURE_NAME_PLACEHOLDER/${FEATURE_NAME}/g" $FEATURE_FOLDER/$FEATURE_NAMECommand.cs
fi

if [ "$COMMAND_OR_QUERY" = "q" ]; then
    cp cmd/templates/Application/queries/Query.sh $FEATURE_FOLDER/$FEATURE_NAME.cs
    sed -i '' "s/FEATURE_NAME_PLACEHOLDER/${FEATURE_NAME}/g" $FEATURE_FOLDER/$FEATURE_NAMEQuery.cs
fi

