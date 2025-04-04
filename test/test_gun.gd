class_name TestGuns
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

const __source = "res://Objects/Scripts/Player.cs"
const scene_link : String = "res://test/test_player_movement_room.tscn"

func test_can_spawn_bullet()->void:
	var runner := scene_runner(scene_link)
	var zone = runner.invoke("find_child", "PosZPassZone")
	await await_millis(300)
	runner.simulate_mouse_button_press(MOUSE_BUTTON_LEFT)
	await await_millis(10) # Allow for bullet instantiation
	var bullet = runner.invoke("get_node", "Player/Body/Gun/Bullet")
	await bullet
