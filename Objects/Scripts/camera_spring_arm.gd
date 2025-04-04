extends Node3D

@export var mouse_sensitivity : float = 0.005
@export_range(0.0, 5, 0.1, "or_greater") var min_camera_dist: float = 1.0
@export_range(-90, 0.0, 0.1, "radians_as_degrees") var min_vertical_angle: float = -PI/2
@export_range(0.0, 90.0, 0.1, "radians_as_degrees") var max_vertical_angle: float = PI/4

@onready var spring_arm := $SpringArm3D
@onready var parent_player := $".."

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _unhandled_input(event : InputEvent):
	if event is InputEventMouseMotion and Input.mouse_mode == Input.MOUSE_MODE_CAPTURED:
		rotation.y -= event.relative.x * mouse_sensitivity
		rotation.y = wrapf(rotation.y, 0.0, TAU)
		
		rotation.x -= event.relative.y * mouse_sensitivity
		rotation.x = clamp(rotation.x, min_vertical_angle, max_vertical_angle)
	
	if event.is_action_pressed("Wheel_Up"):
		spring_arm.spring_length = maxf(spring_arm.spring_length - 1, min_camera_dist)
	if event.is_action_pressed("Wheel_Down"):
		spring_arm.spring_length += 1
		
	if event.is_action_pressed("Toggle_Mouse_Capture"):
		if Input.mouse_mode == Input.MOUSE_MODE_CAPTURED:
			Input.set_mouse_mode(Input.MOUSE_MODE_VISIBLE)
		else:
			Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _process(_delta):
	global_position = parent_player.global_position
