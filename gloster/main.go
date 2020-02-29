package main

import (
	"errors"
	"fmt"
	"strings"

	"github.com/go-kit/kit/endpoint"
)

type StringService interface {
	Uppercase(string) (string, error)
	Count(string) int
}

type stringService struct {	}

var ErrEmpty = errors.New("empty string")

func (stringService) Uppercase(s string) (string, error) {
	if s == "" {
		return "", ErrEmpty
	}
	return strings.ToUpper(s), nil
}

func (stringService) Count(s string) int {
	return len(s)
}

type uppercaseRequest struct {
	S string `json:"s"`
}

type uppercaseResponse struct {
	V string `json:"v"`
	Err string `json:"err,omitempty"`
}

type countRequest struct {
	S string `json:"s"`
}

type countResponse struct {
	V int `json:"v"`
}

func makeUppercaseEndpoint(svc StringService) endpoint.Endpoint {

}

func main() {
	fmt.Println("Hello world")
}