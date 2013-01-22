#!/bin/sh

if [ `grep -c '00.$' "$1"` -eq "$2" ]; then
    exit 0
else
    exit 1
fi
