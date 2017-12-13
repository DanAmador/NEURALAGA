from docopt import docopt

import os
from unityagents import UnityEnvironment
from pprint import pprint

_USAGE = '''
Usage:
  neat (<env>) [options]

Options:

  --train                    Whether to train model, or only run inference [default: False].
'''

options = docopt(_USAGE)
print(options)

# General parameters
model_path = '.\models\\neat'
summary_path = '.\summaries\\neat'
train_model = options['--train']
env_name = options['<env>']

if not os.path.exists(model_path):
    os.makedirs(model_path)

if not os.path.exists(summary_path):
    os.makedirs(summary_path)

env = UnityEnvironment(file_name = env_name, worker_id = 0, curriculum = None)
print(str(env))
brain_name = env.external_brain_names[0]
print(brain_name)
brain = env.brains[brain_name]

info = env.reset(train_mode = train_model, progress = None)[brain_name]
is_continuous = (brain.action_space_type == "continuous")
use_observations = (brain.number_observations > 0)
use_states = (brain.state_space_size > 0)

steps = 0
while steps <= 1e6 or not train_model:
    if env.global_done:
        info = env.reset(train_mode = train_model, progress = None)[brain_name]

    # Get states, 24 of them
    states = info.states[0]

    # Decide and take an action
    env.step(action=[[4]])

    if train_model:

        steps += 1
