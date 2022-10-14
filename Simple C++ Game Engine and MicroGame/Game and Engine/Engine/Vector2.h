#pragma once
#include <math.h>
class Vector2
{
public:
	float x;
	float y;

	Vector2();
	Vector2(float xin, float yin);

	~Vector2();

	float Magnitude();

	Vector2& Add(const Vector2& vec);
	Vector2& Subtract(const Vector2& vec);
	Vector2& Multiply(const Vector2& vec);
	Vector2& Divide(const Vector2& vec);

	Vector2 Normalized();


	friend Vector2& operator+(Vector2& v1, const Vector2& v2);
	friend Vector2& operator-(Vector2& v1, const Vector2& v2);
	friend Vector2& operator*(Vector2& v1, const Vector2& v2);
	friend Vector2& operator/(Vector2& v1, const Vector2& v2);

	Vector2& operator+=(const Vector2& vec);
	Vector2& operator-=(const Vector2& vec);
	Vector2& operator*=(const Vector2& vec);
	Vector2& operator/=(const Vector2& vec);
};

