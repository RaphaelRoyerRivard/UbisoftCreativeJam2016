#pragma strict
 
public var center : Transform;
public var axis   : Vector3 = Vector3.up;
public var radius = 2.0;
public var radiusSpeed = 0.5;
public var rotationSpeed = 80.0; 
 
function Start() {
    transform.position = (transform.position - center.position).normalized * radius + center.position;
}
 
function Update() {
    transform.RotateAround (center.position, axis, rotationSpeed * Time.deltaTime);
    var desiredPosition = (transform.position - center.position).normalized * radius + center.position;
    transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
}