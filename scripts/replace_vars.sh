#!/bin/bash

FILE="appsettings.Production.json"

while IFS= read -r line
do
  while [[ "$line" =~ (\$\{[a-zA-Z_][a-zA-Z0-9_]*\}) ]]; do
    var_name="${BASH_REMATCH[1]:2:-1}"
    
    var_value=$(printenv "$var_name")
    
    if [ -n "$var_value" ]; then
      line="${line//\$\{$var_name\}/$var_value}"
    else
      break
    fi
  done
  
  echo "$line"
done < "$FILE" > "$FILE.tmp"

mv "$FILE.tmp" "$FILE"