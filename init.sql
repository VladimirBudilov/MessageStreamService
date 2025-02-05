-- init.sql
CREATE TABLE IF NOT EXISTS Messages (
                                        Id SERIAL PRIMARY KEY,
                                        Text VARCHAR(128) NOT NULL,
    Timestamp TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );
