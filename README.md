# Depersonalizing Your Test Data

<img align="left" src="https://www.clevercomponents.com/images/DataDepersonalizer-250.jpg" />

If you need to use real production data to test applications, any sensitive data should be removed before loading it into the development environment.

There are different methods for depersonalizing or anonymizing data: replacement, scrambling, masking, blurring, encryption, and etc. Some of these methods can be sometimes reversible; the others may break the structured data integrity.

This article provides a simple replacement algorithm that can depersonalize both structured and non-structured data, including XML, Email messages, and SQL scripts.
