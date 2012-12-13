#!/bin/sh

if [ `grep -c '0\.[0-9]\{2\}$' "$1"` -eq "$2" ]; then
    exit 0
else
    exit 1
fi
