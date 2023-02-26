package com.edgar.aseregehtmlframework.api;

import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;

public interface api {
    void handle(HttpExchange t) throws IOException;
}
