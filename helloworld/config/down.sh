#!/bin/bash

for filename in ./*.yaml; do
    kubectl delete -f $filename
done

sleep 1s