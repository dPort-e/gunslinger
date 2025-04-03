class_name TestPlayer
extends GdUnitTestSuite
@warning_ignore('unused_parameter')
@warning_ignore('return_value_discarded')

const __source = "res://Objects/Scripts/Player.cs"
const scene_link : String = "res://test/test_player_movement_room.tscn"

func test_can_jump()->void:
	var runner := scene_runner(scene_link)
	var player = runner.invoke("find_child", "Player")
	var zone = runner.invoke("find_child", "JumpPassZone")
	await await_millis(500)
	runner.simulate_key_press(KEY_SPACE)
	await assert_signal(zone).wait_until(1000).is_emitted("body_entered", [player])

func test_can_move_forward()->void:
	var runner := scene_runner(scene_link)
	var player = runner.invoke("find_child", "Player")
	var zone = runner.invoke("find_child", "ForwardPassZone")
	await await_millis(500)
	runner.simulate_key_press(KEY_W)
	await assert_signal(zone).wait_until(1000).is_emitted("body_entered", [player])
	
func test_can_move_backward()->void:
	var runner := scene_runner(scene_link)
	var player = runner.invoke("find_child", "Player")
	var zone = runner.invoke("find_child", "BackwardPassZone")
	await await_millis(500)
	runner.simulate_key_press(KEY_S)
	await assert_signal(zone).wait_until(1000).is_emitted("body_entered", [player])
	
func test_can_move_left()->void:
	var runner := scene_runner(scene_link)
	var player = runner.invoke("find_child", "Player")
	var zone = runner.invoke("find_child", "LeftPassZone")
	await await_millis(500)
	runner.simulate_key_press(KEY_A)
	await assert_signal(zone).wait_until(1000).is_emitted("body_entered", [player])
	
func test_can_move_right()->void:
	var runner := scene_runner(scene_link)
	var player = runner.invoke("find_child", "Player")
	var zone = runner.invoke("find_child", "RightPassZone")
	await await_millis(500)
	runner.simulate_key_press(KEY_D)
	await assert_signal(zone).wait_until(1000).is_emitted("body_entered", [player])
