# Collaboration Guide

## Ground rules
We shall not push commits directly to the Master branch. All development should be done in individual branches, that are later merged into Master. 
The Master branch should always have runnable code. It is our main source of truth. 

## How to add a feature
1. Create Issue for the feature you want to work on
2. Move Issue to the To-do column in project 'work', and assign yourself to it 
3. Go to Visual Studio, and create a new branch
3. Write code, stage changes, commit, and push branch 
4. Make Pull Request from branch to Master
5. Write closes #issue-number in the Pull Request's description
6. 1. Merge pull request to Master if there are no merge conflicts 
   2. If there are merge conflicts solve them locally in Visual Studio

## How to solve merge conflict in Visual Studio
1. Pull the latest Master
2. While in branch, right click on Master branch and select 'Merge into current branch'
3. Open the conflicted files from the Git Changes window
4. Use the Merge Editor to help with the conflict and then clik Apply merge
5. Commit the merge, and push 
6. Re-check if the merge conflict is solved in the Pull Request view on GitHub
