from __future__ import print_function
from docopt import docopt
from unityagents import UnityEnvironment
from pprint import pprint
from numpy import argmax
import os
import neat

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
config_path = '.\\NEAT\config'
train_model = options['--train']
env_name = options['<env>']

if not os.path.exists(model_path):
    os.makedirs(model_path)

if not os.path.exists(summary_path):
    os.makedirs(summary_path)

env = UnityEnvironment(file_name = env_name, worker_id = 0, curriculum = None)
brain_name = env.external_brain_names[0]
brain = env.brains[brain_name]
print(str(env))

# NEAT
def eval_genomes(genomes, config):
    for genome_id, genome in genomes:
        net = neat.nn.FeedForwardNetwork.create(genome, config)

        info = env.reset(train_mode = train_model, progress = None)[brain_name]

        while not info.local_done:
            # Get states, 24 of them
            states = info.states[0]

            # Get NN (max) output
            output = net.activate(states)
            action = argmax(output)

            # Take action
            info = env.step(action = [[action]])[brain_name]

        # Update fitness
        genome.fitness = info.rewards[0]

def run(config_file):
    # Load configuration.
    config = neat.Config(neat.DefaultGenome, neat.DefaultReproduction,
                         neat.DefaultSpeciesSet, neat.DefaultStagnation,
                         config_file)

    # Create the population, which is the top-level object for a NEAT run.
    p = neat.Population(config)

    # Add a stdout reporter to show progress in the terminal.
    p.add_reporter(neat.StdOutReporter(True))
    stats = neat.StatisticsReporter()
    p.add_reporter(stats)
    p.add_reporter(neat.Checkpointer(5))

    # Run for up to 300 generations.
    winner = p.run(eval_genomes, 300)

    # Display the winning genome.
    print('\nBest genome:\n{!s}'.format(winner))

    p = neat.Checkpointer.restore_checkpoint('neat-checkpoint-4')

    # Run winner
    winner_net = neat.nn.FeedForwardNetwork.create(winner, config)

    while not env.global_done:
        info = env.reset(train_mode = False, progress = None)[brain_name]

        while not info.local_done:
            # Get states, 24 of them
            states = info.states[0]

            # Get NN (max) output
            output = winner_net.activate(states)
            action = argmax(output)

            # Take action
            info = env.step(action = [[action]])[brain_name]

    env.close()

run(config_path)
